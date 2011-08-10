using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace tinyweb.framework
{
    public class DefaultHandlerScanner : IHandlerScanner
    {
        public IEnumerable<HandlerData> FindAll(IEnumerable<Type> types)
        {
            var handlers = types.Select(type => new { Route = getRoute(type), Type = type }).ToList();

            return handlers.Select(t => new HandlerData
            {
                Type = t.Type,
                Uri = t.Route.RouteUri,
                DefaultRouteValues = t.Route.Defaults
            });
        }

        public Func<Type, bool> GetSearcher()
        {
            return t => t.Name.ToLower().EndsWith("handler") && handlerIsValid(t);
        }

        private bool handlerIsValid(Type type)
        {
            var allowedMethods = new[] { "get", "post", "put", "delete" };
            return type.GetMethods().Any(method => allowedMethods.Contains(method.Name.ToLower()) && method.ReturnType == typeof(IResult));
        }

        private Route getRoute(Type type)
        {
            Route route;
            var routeMember = type.GetMember("Route", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase).SingleOrDefault();

            if (routeMember != null)
            {
                var handler = HandlerFactory.Current.Create(new HandlerData { Type = type });
                route = getExplicitRouteFromHandler(routeMember, handler) ?? new Route(getRouteUriByConvention(type));

                if (route.RouteUri == "/")
                {
                    route.RouteUri = String.Empty;
                }
                else if (route.RouteUri.IsEmpty())
                {
                    route.RouteUri = getRouteUriByConvention(type);
                }
            }
            else
            {
                route = new Route(getRouteUriByConvention(type));
            }

            route.RouteUri = addHandlerAreaToRouteUriIfRegistered(type, route.RouteUri);

            return route;
        }

        private Route getExplicitRouteFromHandler(MemberInfo routeMember, object handler)
        {
            Route route = null;

            if (routeMember is MethodInfo)
            {
                route = ((MethodInfo)routeMember).Invoke(handler, null) as Route;
            }

            if (routeMember is FieldInfo)
            {
                route = ((FieldInfo)routeMember).GetValue(handler) as Route;
            }

            if (routeMember is PropertyInfo)
            {
                route = ((PropertyInfo)routeMember).GetValue(handler, null) as Route;
            }

            return route;
        }

        private string getRouteUriByConvention(Type type)
        {
            var handlerStart = type.Name.ToLower().IndexOf("handler");
            var pascalConvention = type.Name.Substring(0, handlerStart);
            
            return pascalConvention.ToLower() == "root" ? String.Empty : String.Join("/", pascalConvention.PascalSplit());
        }

        private string addHandlerAreaToRouteUriIfRegistered(Type handlerType, string routeUri)
        {
            var handlerNamespace = handlerType.Namespace ?? string.Empty;
            var handlerArea = Tinyweb.Areas.ContainsKey(handlerNamespace) ? Tinyweb.Areas[handlerNamespace] : null;

            if (handlerArea.IsEmpty() || handlerArea == routeUri)
            {
                return routeUri;
            }

            var areaRouteUriPrefix = handlerArea + "/";

            if (routeUri.StartsWith(areaRouteUriPrefix))
            {
                return routeUri;
            }

            return areaRouteUriPrefix + routeUri;
        }
    }
}