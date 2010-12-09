using StructureMap;

namespace tinyweb.framework
{
    public class DefaultHandlerFactory : IHandlerFactory
    {
        public object Create(HandlerData handlerData)
        {
            return ObjectFactory.GetInstance(handlerData.Type);
        }
    }
}