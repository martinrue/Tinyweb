using System;
using System.Web.Routing;

namespace tinyweb.framework.tests
{
    public class CustomFilterInvoker : IFilterInvoker
    {
        public IHandlerResult RunBefore(object filter, RequestContext requestContext)
        {
            throw new NotImplementedException();
        }

        public IHandlerResult RunAfter(object filter, RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}