using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IHandlerInvoker
    {
        ExecutionResult Execute(object handler, RequestContext requestContext, HandlerData handlerData);
    }
}