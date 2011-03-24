using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public class HttpHandler : IHttpHandler
    {
        RequestContext _requestContext;
        HandlerData _handlerData;

        public bool IsReusable { get { return false; } }

        public HttpHandler(RequestContext requestContext, HandlerData handlerData)
        {
            _requestContext = requestContext;
            _handlerData = handlerData;
        }

        public void ProcessRequest(HttpContext context)
        {
            var handler = HandlerFactory.Current.Create(_handlerData);
            var result = HandlerInvoker.Current.Execute(handler, _requestContext);

            result.ProcessResult(new DefaultRequestContext(context), new DefaultResponseContext(context));
        }
    }
}