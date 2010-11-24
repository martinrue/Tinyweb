using System;

namespace tinyweb.framework
{
    public class DefaultHandlerFactory : IHandlerFactory
    {
        public object Create(HandlerData handlerData)
        {
            try
            {
                return Activator.CreateInstance(handlerData.Type);
            }
            catch (MissingMethodException)
            {
                throw new NoParameterlessConstructorException(String.Format("No parameterless constructor found for type {0}", handlerData.Type.ToString()));
            }
        }
    }
}