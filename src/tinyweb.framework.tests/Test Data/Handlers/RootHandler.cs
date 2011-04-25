namespace tinyweb.framework.tests
{
    public class RootHandler
    {
        public IResult Get()
        {
            return new StringResult("Get");
        }
    }
}