namespace tinyweb.framework.tests
{
    public class Resource3Handler
    {
        Route route = new Route("resource3");

        public IHandlerResult Get()
        {
            return new StringResult("");
        }
    }
}