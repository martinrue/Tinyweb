namespace tinyweb.framework.tests
{
    public class ExplicitRootHandler
    {
        Route route = new Route("/");

        public IResult Get()
        {
            return new StringResult("Get");
        }
    }
}