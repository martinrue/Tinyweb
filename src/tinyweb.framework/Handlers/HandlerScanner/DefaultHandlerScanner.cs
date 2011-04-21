using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace tinyweb.framework
{
    public class DefaultHandlerScanner : IHandlerScanner
    {
        public IEnumerable<HandlerData> FindAll()
        {
            var types = findHandlers();

            return types.Select(type => new { Route = getRoute(type), Type = type })
                        .Select(t => new HandlerData
                        {
                            Type = t.Type,
                            Uri = t.Route.RouteUri,
                            DefaultRouteValues = t.Route.Defaults
                        });
        }

        private IEnumerable<Type> findHandlers()
        {
            var typesFound = new List<Type>();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(assembly =>
            {
                typesFound.AddRange(assembly.GetTypes().Where(t => t.Name.ToLower().EndsWith("handler") && handlerIsValid(t)));
            });

            return typesFound;
        }

        private bool handlerIsValid(Type type)
        {
            var allowedMethods = new[] { "get", "post", "put", "delete" };
            return type.GetMethods().Any(method => allowedMethods.Contains(method.Name.ToLower()) && method.ReturnType == typeof(IHandlerResult));
        }

        private Route getRoute(Type type)
        {
            var handler = HandlerFactory.Current.Create(new HandlerData { Type = type });
            var routeProperty = handler.GetType().GetField("route", BindingFlags.NonPublic | BindingFlags.Instance);

            if (routeProperty == null)
            {
                return new Route(getRouteUriByConvention(type));
            }

            var route = routeProperty.GetValue(handler) as Route;

            if (route.RouteUri == "/")
            {
                route.RouteUri = String.Empty;
            }
            else if (route.RouteUri.IsEmpty())
            {
                route.RouteUri = getRouteUriByConvention(type);
            }

            return route;
        }

        private string getRouteUriByConvention(Type type)
        {
            var handlerStart = type.Name.ToLower().IndexOf("handler");
            var pascalConvention = type.Name.Substring(0, handlerStart);

            return pascalConvention.ToLower() == "root" ? String.Empty : String.Join("/", pascalConvention.PascalSplit());
        }
    }
}