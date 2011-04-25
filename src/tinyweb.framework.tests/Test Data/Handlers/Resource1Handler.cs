namespace tinyweb.framework.tests
{
    public class Resource1Handler
    {
        Route route = new Route("resource1", new { route1 = "default1", route2 = "default2" });

        public IResult Get()
        {
            return new StringResult("Get");
        }

        public IResult Post()
        {
            return new StringResult("Post");
        }
    }
}