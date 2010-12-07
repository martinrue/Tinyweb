using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public class RouteHandler : IRouteHandler
    {
        HandlerData handlerData;

        public RouteHandler(HandlerData handlerData)
        {
            this.handlerData = handlerData;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler(requestContext, handlerData);
        }
    }
}