using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IFilterInvoker
    {
        IResult RunBefore(object filter, RequestContext requestContext);
        IResult RunAfter(object filter, RequestContext requestContext);
    }
}