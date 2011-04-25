using System.Collections.Generic;
using System.Linq;
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
            var beforeFilters = createBeforeFilters();
            var afterFilters = createAfterFilters();

            processBeforeFilters(beforeFilters, context);

            var handler = HandlerFactory.Current.Create(_handlerData);
            var result = HandlerInvoker.Current.Execute(handler, _requestContext);

            if (result.BeforeResult != null)
            {
                result.BeforeResult.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));    
            }
            
            result.Result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));

            if (result.AfterResult != null)
            {
                result.AfterResult.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
            }

            processAfterFilters(afterFilters, context);
        }

        private void processBeforeFilters(IEnumerable<object> filters, HttpContext context)
        {
            filters.ForEach(type =>
            {
                var result = FilterInvoker.Current.RunBefore(type, _requestContext);

                if (result != null)
                {
                    result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }
            });
        }

        private void processAfterFilters(IEnumerable<object> filters, HttpContext context)
        {
            filters.ForEach(type =>
            {
                var result = FilterInvoker.Current.RunAfter(type, _requestContext);

                if (result != null)
                {
                    result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }
            });
        }

        private IEnumerable<object> createBeforeFilters()
        {
            return Tinyweb.Filters.Where(f => f.BeforeFilter).OrderBy(f => f.Priority).Select(filter => FilterFactory.Current.Create(filter));
        }

        private IEnumerable<object> createAfterFilters()
        {
            return Tinyweb.Filters.Where(f => f.AfterFilter).OrderBy(f => f.Priority).Select(filter => FilterFactory.Current.Create(filter));
        }
    }
}