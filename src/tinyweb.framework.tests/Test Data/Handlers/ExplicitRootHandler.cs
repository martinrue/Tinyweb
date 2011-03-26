namespace tinyweb.framework.tests
{
    public class ExplicitRootHandler
    {
        Route route = new Route("/");

        public IHandlerResult Get()
        {
            return new StringResult("Get");
        }
    }
}