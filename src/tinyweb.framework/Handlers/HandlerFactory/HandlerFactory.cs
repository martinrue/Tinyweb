namespace tinyweb.framework
{
    public static class HandlerFactory
    {
        static IHandlerFactory factory = new ActivatorHandlerFactory();

        public static IHandlerFactory Current
        {
            get { return factory; }
        }

        public static void SetHandlerFactory(IHandlerFactory handlerFactory)
        {
            factory = handlerFactory;
        }
    }
}