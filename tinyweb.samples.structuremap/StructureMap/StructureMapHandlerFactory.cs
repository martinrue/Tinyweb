using StructureMap;
using tinyweb.framework;

namespace tinyweb.samples.structuremap
{
    public class StructureMapHandlerFactory : IHandlerFactory
    {
        public object Create(HandlerData handlerData)
        {
            return ObjectFactory.GetInstance(handlerData.Type);
        }
    }
}