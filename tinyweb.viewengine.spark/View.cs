namespace tinyweb.viewengine.spark
{
    public static class View
    {
        public static SparkResult<T> Spark<T>(T model, string templatePath, string master = null)
        {
            return new SparkResult<T>(model, templatePath, master);
        }

        public static SparkResult Spark(string templatePath, string master = null)
        {
            return new SparkResult(templatePath, master);
        }
    }
}