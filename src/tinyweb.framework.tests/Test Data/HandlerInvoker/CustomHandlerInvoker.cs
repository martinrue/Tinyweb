using System;
using System.Web.Routing;

namespace tinyweb.framework.tests
{
    public class CustomHandlerInvoker : IHandlerInvoker
    {
        public ExecutionResult Execute(object handler, RequestContext requestContext, HandlerData handlerData)
        {
            throw new NotImplementedException();
        }
    }
}