namespace tinyweb.framework
{
    public interface IHandlerFactory
    {
        object Create(HandlerData handlerData);
    }
}