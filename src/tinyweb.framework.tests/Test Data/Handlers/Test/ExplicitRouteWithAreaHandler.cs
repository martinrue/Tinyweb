namespace tinyweb.framework.tests.TestArea
{
    public class ExplicitRouteWithAreaHandler
    {
        public Route Route()
        {
            return new Route("test/foo/baz");
        }
        
        public IResult Get()
        {
            return new StringResult("Get");
        } 
    }
}