using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IFilterInvoker
    {
        IResult RunBefore(object filter, RequestContext requestContext, HandlerData handlerData);
        IResult RunAfter(object filter, RequestContext requestContext, HandlerData handlerData);
    }
}