using System.Web.Routing;

namespace tinyweb.framework
{
    public class RouteValues: IRouteValues
    {
        public RouteValueDictionary Routes { get; set; }

        public RouteValues(RouteValueDictionary routeValues)
        {
            Routes = routeValues;
        }
    }
}