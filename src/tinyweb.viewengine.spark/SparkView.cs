using System.Web;
using Spark;

namespace tinyweb.viewengine.spark
{
    public abstract class SparkView<T> : AbstractSparkView
    {
        public T Model { get; set; }

        public string H(object value)
        {
            return HttpUtility.HtmlEncode(value.ToString());
        } 
    }

    public abstract class SparkView : AbstractSparkView
    {
        public string H(object value)
        {
            return HttpUtility.HtmlEncode(value.ToString());
        }
    }
}