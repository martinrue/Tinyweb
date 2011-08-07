namespace tinyweb.framework.tests.TestArea
{
    public class TestHandler
    {
        public IResult Get()
        {
            return new StringResult("Get");
        } 
    }
}