namespace tinyweb.framework
{
    public static class HandlerInvoker
    {
        static IHandlerInvoker invoker = new DefaultHandlerInvoker(new ArgumentBuilder());

        public static IHandlerInvoker Current
        {
            get { return invoker; }
        }

        public static void SetHandlerInvoker(IHandlerInvoker handlerInvoker)
        {
            invoker = handlerInvoker;
        }
    }
}