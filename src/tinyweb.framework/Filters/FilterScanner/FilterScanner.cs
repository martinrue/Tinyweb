namespace tinyweb.framework
{
    public static class FilterScanner
    {
        static IFilterScanner scanner = new DefaultFilterScanner();

        public static IFilterScanner Current
        {
            get { return scanner; }
        }

        public static void SetFilterScanner(IFilterScanner filterScanner)
        {
            scanner = filterScanner;
        }
    }
}