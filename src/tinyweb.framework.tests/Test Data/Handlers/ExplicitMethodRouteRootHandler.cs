namespace tinyweb.framework.tests
{
    public class ExplicitMethodRouteRootHandler
    {
        public Route Route()
        {
            return new Route("/");
        }

        public IResult Get()
        {
            return new StringResult("Get");
        }
    }
}