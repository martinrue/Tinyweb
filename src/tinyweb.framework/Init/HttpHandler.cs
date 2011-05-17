using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace tinyweb.framework
{
    public class HttpHandler : IHttpHandler, IRequiresSessionState
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
            try
            {
                var beforeFilters = createBeforeFilters();
                var afterFilters = createAfterFilters();

                processBeforeFilters(beforeFilters, context, _handlerData);

                var handler = HandlerFactory.Current.Create(_handlerData);
                var result = HandlerInvoker.Current.Execute(handler, _requestContext, _handlerData);

                if (result.BeforeResult != null)
                {
                    result.BeforeResult.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }

                result.Result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));

                if (result.AfterResult != null)
                {
                    result.AfterResult.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }

                processAfterFilters(afterFilters, context, _handlerData);
            }
            catch (ThreadAbortException exception)
            {
                // Swallow exceptions caused by Result.Redirect calls in filters/handlers
            }
            catch (Exception exception)
            {
                if (Tinyweb.OnError != null)
                {
                    Tinyweb.OnError(exception, _requestContext, _handlerData);
                }
                
                throw;
            }
        }

        private void processBeforeFilters(IEnumerable<object> filters, HttpContext context, HandlerData handlerData)
        {
            filters.ForEach(type =>
            {
                var result = FilterInvoker.Current.RunBefore(type, _requestContext, handlerData);

                if (result != null)
                {
                    result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }
            });
        }

        private void processAfterFilters(IEnumerable<object> filters, HttpContext context, HandlerData handlerData)
        {
            filters.ForEach(type =>
            {
                var result = FilterInvoker.Current.RunAfter(type, _requestContext, handlerData);

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