using System.Linq;
using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public class HttpHandler : IHttpHandler
    {
        RequestContext requestContext;
        HandlerData handlerData;

        public bool IsReusable { get { return false; } }

        public HttpHandler(RequestContext requestContext, HandlerData handlerData)
        {
            this.requestContext = requestContext;
            this.handlerData = handlerData;
        }

        public void ProcessRequest(HttpContext context)
        {
            var handler = HandlerFactory.Current.Create(handlerData);
            var result = HandlerInvoker.Current.Execute(handler, requestContext);

            context.Response.ContentType = result.ContentType;
            result.CustomHeaders.ForEach(header => context.Response.AddHeader(header.Key, header.Value));
       
            if (result.IsFileResult)
            {
                context.Response.WriteFile(result.GetResult());
            }
            else
            {
                context.Response.Write(result.GetResult());
            }
        }
    }
}