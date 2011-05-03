using System.Reflection;
using System.Web.Routing;

namespace tinyweb.framework
{
    public interface IArgumentBuilder
    {
        object[] BuildArguments(ParameterInfo[] parameters, RequestContext requestContext, HandlerData handlerData);
    }
}