namespace tinyweb.framework.tests
{
    public class Resource1Handler
    {
        Route route = new Route("resource1", new { route1 = "default1", route2 = "default2" });

        public IHandlerResult Get()
        {
            return new StringResult("Get");
        }

        public IHandlerResult Post()
        {
            return new StringResult("Post");
        }
    }
}