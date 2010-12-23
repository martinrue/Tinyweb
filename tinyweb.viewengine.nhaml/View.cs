namespace tinyweb.viewengine.nhaml
{
    public static class View
    {
        public static NHamlResult<T> NHaml<T>(T model, params string[] templates)
        {
            return new NHamlResult<T>(model, templates);
        }

        public static NHamlResult NHaml(params string[] templates)
        {
            return new NHamlResult(templates);
        }
    }
}