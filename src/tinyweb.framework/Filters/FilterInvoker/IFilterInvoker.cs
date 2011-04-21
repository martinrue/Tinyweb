using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IFilterInvoker
    {
        IHandlerResult RunBefore(object filter, RequestContext requestContext);
        IHandlerResult RunAfter(object filter, RequestContext requestContext);
    }
}