namespace tinyweb.framework.tests
{
    public class Resource1Handler
    {
        Route route = new Route("resource1", new { route1 = "default1", route2 = "default2" });

        public StringResult Get()
        {
            return "Get";
        }

        public StringResult Post()
        {
            return "Post";
        }
    }
}