using System;

namespace tinyweb.framework
{
    public class ActivatorHandlerFactory : IHandlerFactory
    {
        public object Create(HandlerData handlerData)
        {
            return Activator.CreateInstance(handlerData.Type);
        }
    }
}