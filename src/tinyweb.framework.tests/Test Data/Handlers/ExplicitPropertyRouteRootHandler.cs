namespace tinyweb.framework.tests
{
    public class ExplicitPropertyRouteRootHandler
    {
        public Route Route
        {
            get { return new Route("/"); }
        }

        public IResult Get()
        {
            return new StringResult("Get");
        }
    }
}