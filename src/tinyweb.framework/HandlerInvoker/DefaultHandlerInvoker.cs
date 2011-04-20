using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public class DefaultHandlerInvoker : IHandlerInvoker
    {
        public IHandlerResult Execute(object handler, RequestContext requestContext)
        {
            var httpVerb = requestContext.HttpContext.Request.HttpMethod.ToEnum<HttpVerb>();
            
            var before = getMethod(handler, "Before");
            var method = getMethod(handler, httpVerb);
            var after = getMethod(handler, "After");

            if (method == null)
            {
                throw new HttpException(HttpStatusCode.NotImplemented.CastInt(), "The request could not be completed because the resource does not support {0}".With(httpVerb.Name()));
            }

            if (before != null)
            {
                var beforeArgs = buildArgumentList(before.GetParameters(), requestContext);
                before.Invoke(handler, beforeArgs);
            }

            var methodArgs = buildArgumentList(method.GetParameters(), requestContext);
            var result = (IHandlerResult)method.Invoke(handler, methodArgs);

            if (after != null)
            {
                var afterArgs = buildArgumentList(after.GetParameters(), requestContext);
                after.Invoke(handler, afterArgs);
            }

            return result;
        }

        private MethodInfo getMethod(object handler, HttpVerb verb)
        {
            return handler.GetType().GetMethods().SingleOrDefault(m => m.Name.ToLower() == verb.Name().ToLower());
        }

        private MethodInfo getMethod(object handler, string name)
        {
            return handler.GetType().GetMethods().SingleOrDefault(m => m.Name.ToLower() == name.ToLower());
        }

        private object[] buildArgumentList(ParameterInfo[] parameters, RequestContext requestContext)
        {
            var argumentDictionary = new Dictionary<string, object>();

            foreach (var parameter in parameters)
            {
                if (argumentDictionary.ContainsKey(parameter.Name))
                {
                    continue;
                }

                if (parameter.ParameterType.IsValueType || parameter.ParameterType == typeof(String))
                {
                    var value = getValueFromRequest(requestContext, parameter.Name.ToLower(), parameter.ParameterType);

                    if (value != null)
                    {
                        argumentDictionary.Add(parameter.Name, value);
                    }
                }
                else if (parameter.ParameterType.IsArray)
                {
                    var value = getArrayValueFromRequest(requestContext, parameter.Name, parameter.ParameterType);

                    if (value != null)
                    {
                        argumentDictionary.Add(parameter.Name, value);
                    }
                }
                else if (parameter.ParameterType.IsClass)
                {
                    if (parameter.ParameterType == typeof(RequestContext))
                    {
                        argumentDictionary.Add(parameter.Name, requestContext);
                    }
                    else
                    {
                        var instance = createAndPopulateInstance(requestContext, parameter.Name, parameter.ParameterType);
                        argumentDictionary.Add(parameter.Name, instance);
                    }
                }

                if (!argumentDictionary.ContainsKey(parameter.Name))
                {
                    throw new Exception(String.Format("No argument was supplied for required parameter {0} ({1})", parameter.Name, parameter.ParameterType));
                }
            }

            return argumentDictionary.Select(p => p.Value).ToArray();
        }

        private object createAndPopulateInstance(RequestContext requestContext, string requestedName, Type requestedType)
        {
            try
            {
                var instance = Activator.CreateInstance(requestedType);

                foreach (var property in requestedType.GetProperties())
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(String))
                    {
                        property.SetValue(instance, getValueFromRequest(requestContext, property.Name, property.PropertyType), null);
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        property.SetValue(instance, getArrayValueFromRequest(requestContext, property.Name, property.PropertyType), null);
                    }
                    else if (property.PropertyType.IsClass)
                    {
                        var childInstance = createAndPopulateInstance(requestContext, property.Name, property.PropertyType);
                        property.SetValue(instance, childInstance, null);
                    }
                }

                return instance;
            }
            catch
            {
                throw new Exception(String.Format("The parameter {0} of type {1} or one of its dependencies does not have a parameterless constructor defined", requestedName, requestedType));
            }
        }

        private object getValueFromRequest(RequestContext requestContext, string requestedName, Type requestedType)
        {
            var value = findValue(requestContext.RouteData.Values, requestedName, requestedType);

            if (value == null)
            {
                value = findValue(requestContext.HttpContext.Request.QueryString.ToRouteValueDictionary(), requestedName, requestedType);
            }

            if (value == null)
            {
                value = findValue(requestContext.HttpContext.Request.Form.ToRouteValueDictionary(), requestedName, requestedType);
            }

            return value;
        }

        private object findValue(RouteValueDictionary dictionary, string requestedName, Type requestedType)
        {
            if (dictionary.ContainsKey(requestedName))
            {
                if (requestedType == typeof(bool) && dictionary[requestedName].ToString().ToLower() == "on")
                {
                    return true;
                }

                try
                {
                    return Convert.ChangeType(dictionary[requestedName], requestedType);
                }
                catch { }
            }

            return null;
        }

        private object getArrayValueFromRequest(RequestContext requestContext, string requestedName, Type requestedType)
        {
            string[] requestArray = null;

            if (requestContext.HttpContext.Request.QueryString != null)
            {
                if (requestContext.HttpContext.Request.QueryString.AllKeys.Contains(requestedName + "[]"))
                {
                    requestArray = requestContext.HttpContext.Request.QueryString.GetValues(requestedName + "[]");
                }
            }

            if (requestContext.HttpContext.Request.Form != null)
            {
                if (requestContext.HttpContext.Request.Form.AllKeys.Contains(requestedName + "[]"))
                {
                    requestArray = requestContext.HttpContext.Request.Form.GetValues(requestedName + "[]");
                }
            }

            if (requestArray == null || requestArray.GetType() == requestedType)
            {
                return requestArray;
            }

            var array = Array.CreateInstance(requestedType.GetElementType(), requestArray.Length);
            var index = 0;

            requestArray.ForEach(element => array.SetValue(Convert.ChangeType(element, requestedType.GetElementType()), index++));

            return array;
        }
    }
}