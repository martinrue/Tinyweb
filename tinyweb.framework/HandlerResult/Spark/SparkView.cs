using System.Web;

namespace tinyweb.framework
{
    public abstract class SparkView<T> : Spark.AbstractSparkView
    {
        public T Model { get; set; }

        public string H(object value)
        {
            return HttpUtility.HtmlEncode(value.ToString());
        } 
    }
}