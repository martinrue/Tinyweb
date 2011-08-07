namespace tinyweb.framework.tests.TestArea
{
    public class ResourceHandler
    {
        public IResult Get()
        {
            return new StringResult("Get");
        } 
    }
}