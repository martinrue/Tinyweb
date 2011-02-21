namespace tinyweb.framework
{
    public static class HandlerScanner
    {
        static IHandlerScanner scanner = new DefaultHandlerScanner();

        public static IHandlerScanner Current
        {
            get { return scanner; }
        }

        public static void SetHandlerScanner(IHandlerScanner handlerScanner)
        {
            scanner = handlerScanner;
        }
    }
}