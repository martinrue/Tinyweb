namespace tinyweb.framework.tests
{
    public class RootHandler
    {
        public IHandlerResult Get()
        {
            return new StringResult("Get");
        }
    }
}