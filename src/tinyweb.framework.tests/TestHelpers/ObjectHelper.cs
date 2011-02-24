using System.Web.Routing;

namespace tinyweb.framework.tests
{
    public static class ObjectExtensions
    {
        public static RouteValueDictionary AsRouteValueDictionary(this object input)
        {
            return new RouteValueDictionary(input);
        }
    }
}