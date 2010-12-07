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

        public static SparkResult<T> Spark<T>(T model, string templatePath)
        {
            return new SparkResult<T>(model, templatePath);
        }

        public static SparkResult Spark(string templatePath)
        {
            return new SparkResult(templatePath);
        }
    }
}