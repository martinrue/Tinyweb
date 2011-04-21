using System;
using System.Web.Routing;

namespace tinyweb.framework.tests
{
    public class CustomHandlerInvoker : IHandlerInvoker
    {
        public ExecutionResult Execute(object handler, RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}