namespace tinyweb.framework
{
    public static class FilterFactory
    {
        static IFilterFactory factory = new DefaultFilterFactory();

        public static IFilterFactory Current
        {
            get { return factory; }
        }

        public static void SetFilterFactory(IFilterFactory filterFactory)
        {
            factory = filterFactory;
        }
    }
}