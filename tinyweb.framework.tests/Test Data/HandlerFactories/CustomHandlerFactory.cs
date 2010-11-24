using System;

namespace tinyweb.framework.tests
{
    public class CustomHandlerFactory : IHandlerFactory
    {
        public object Create(HandlerData handlerData)
        {
            throw new NotImplementedException();
        }
    }
}