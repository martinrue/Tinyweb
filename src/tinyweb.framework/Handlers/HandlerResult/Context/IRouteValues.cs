using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IRouteValues
    {
        RouteValueDictionary Routes { get; set; }
    }
}