namespace tinyweb.framework.tests.TestArea
{
    public class ExplicitRouteAreaHandler
    {
        public Route Route()
        {
            return new Route("foo/bar");
        }
        
        public IResult Get()
        {
            return new StringResult("Get");
        } 
    }
}