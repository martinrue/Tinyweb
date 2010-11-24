namespace tinyweb.framework.tests
{
    [Accept("resource1")]
    public class Resource1Handler
    {
        object defaults = new { route1 = "default1", route2 = "default2" };

        public HtmlResult Get()
        {
            return "Get";
        }

        public HtmlResult Post()
        {
            return "Post";
        }
    }
}