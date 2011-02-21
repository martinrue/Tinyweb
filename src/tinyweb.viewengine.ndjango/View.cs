namespace tinyweb.viewengine.ndjango
{
    public static class View
    {
        public static NDjangoResult<T> NDjango<T>(T model, string template)
        {
            return new NDjangoResult<T>(model, template);
        }

        public static NDjangoResult NDjango(string template)
        {
            return new NDjangoResult(template);
        }
    }
}