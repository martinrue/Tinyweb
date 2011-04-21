using System.Web.Routing;

namespace tinyweb.framework
{
    public class DefaultRequestContext : IRequestContext
    {
        public IRequestHeaders Headers { get; private set; }
        public IRouteValues RouteValues { get; private set; }

        public DefaultRequestContext(RequestContext context)
        {
            Headers = new RequestHeaders(context.HttpContext);
            RouteValues = new RouteValues(context.RouteData.Values);
        }
    }
}