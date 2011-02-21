using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IHandlerInvoker
    {
        IHandlerResult Execute(object handler, RequestContext requestContext);
    }
}