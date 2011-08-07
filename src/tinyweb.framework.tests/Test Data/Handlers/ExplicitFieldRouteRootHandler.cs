namespace tinyweb.framework.tests
{
    public class ExplicitFieldRouteRootHandler
    {
        Route route = new Route("/");

        public IResult Get()
        {
            return new StringResult("Get");
        }
    }
}