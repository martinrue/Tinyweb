namespace tinyweb.framework
{
    public static class FilterInvoker
    {
        static IFilterInvoker invoker = new DefaultFilterInvoker(new ArgumentBuilder());

        public static IFilterInvoker Current
        {
            get { return invoker; }
        }

        public static void SetFilterInvoker(IFilterInvoker filterInvoker)
        {
            invoker = filterInvoker;
        }
    }
}