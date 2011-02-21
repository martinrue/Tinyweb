namespace tinyweb.framework.tests
{
    public class Resource2Handler
    {
        Route route = new Route("resource2");
    
        public IHandlerResult Get()
        {
            return new StringResult("");
        }
    }
}