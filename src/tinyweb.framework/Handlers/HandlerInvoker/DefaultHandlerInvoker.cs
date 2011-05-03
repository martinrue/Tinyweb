using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public class DefaultHandlerInvoker : IHandlerInvoker
    {
        readonly IArgumentBuilder _argumentBuilder;

        public DefaultHandlerInvoker(IArgumentBuilder argumentBuilder)
        {
            _argumentBuilder = argumentBuilder;
        }

        public ExecutionResult Execute(object handler, RequestContext requestContext, HandlerData handlerData)
        {
            var result = new ExecutionResult();
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
                var beforeArgs = _argumentBuilder.BuildArguments(before.GetParameters(), requestContext, handlerData);
                result.BeforeResult = before.Invoke(handler, beforeArgs) as IResult;
            }

            var methodArgs = _argumentBuilder.BuildArguments(method.GetParameters(), requestContext, handlerData);
            result.Result = (IResult) method.Invoke(handler, methodArgs);

            if (after != null)
            {
                var afterArgs = _argumentBuilder.BuildArguments(after.GetParameters(), requestContext, handlerData);
                result.AfterResult = after.Invoke(handler, afterArgs) as IResult;
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
    }
}