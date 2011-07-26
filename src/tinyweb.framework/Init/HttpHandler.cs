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
                var globalFilters = createFilters();

                processBeforeFilters(globalFilters, context, _handlerData);

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

                processAfterFilters(globalFilters, context, _handlerData);
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

        private void processBeforeFilters(IEnumerable<FilterInstance> filters, HttpContext context, HandlerData handlerData)
        {
            filters.Where(filter => filter.BeforeFilter).Select(filter => filter.Instance).ForEach(instance =>
            {
                var result = FilterInvoker.Current.RunBefore(instance, _requestContext, handlerData);

                if (result != null)
                {
                    result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }
            });
        }

        private void processAfterFilters(IEnumerable<FilterInstance> filters, HttpContext context, HandlerData handlerData)
        {
            filters.Where(filter => filter.AfterFilter).Select(filter => filter.Instance).ForEach(instance =>
            {
                var result = FilterInvoker.Current.RunAfter(instance, _requestContext, handlerData);

                if (result != null)
                {
                    result.ProcessResult(new DefaultRequestContext(_requestContext), new DefaultResponseContext(context));
                }
            });
        }

        private IEnumerable<FilterInstance> createFilters()
        {
            return Tinyweb.Filters.OrderBy(f => f.Priority).Select(filter => new FilterInstance 
            { 
                Instance = FilterFactory.Current.Create(filter),
                BeforeFilter = filter.BeforeFilter,
                AfterFilter = filter.AfterFilter
            
            }).ToList();
        }
    }
}