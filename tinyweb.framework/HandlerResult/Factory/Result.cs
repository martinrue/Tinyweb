namespace tinyweb.framework
{
    public static class Result
    {
        public static HtmlResult Html(string filepath)
        {
            return new HtmlResult(filepath);
        }

        public static FileResult File(string filepath)
        {
            return new FileResult(filepath);
        }

        public static JsonResult Json(object data)
        {
            return new JsonResult(data);
        }

        public static StringResult String(string data)
        {
            return new StringResult(data);
        }

        public static SparkResult<T> Spark<T>(T model, string templatePath, string master = null)
        {
            return new SparkResult<T>(model, templatePath, master);
        }

        public static SparkResult Spark(string templatePath, string master = null)
        {
            return new SparkResult(templatePath, master);
        }

        public static NHamlResult<T> NHaml<T>(T model, params string[] templates)
        {
            return new NHamlResult<T>(model, templates);
        }

        public static NHamlResult NHaml(params string[] templates)
        {
            return new NHamlResult(templates);
        }

        public static RedirectResult<T> Redirect<T>()
        {
            return new RedirectResult<T>();
        }

        public static RedirectResult Redirect(string uri)
        {
            return new RedirectResult(uri);
        }
    }
}