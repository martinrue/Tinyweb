using tinyweb.framework;

namespace tinyweb.viewengine.tests
{
    public class TestHandler
    {
        public IHandlerResult Get()
        {
            return Result.String("");
        }
    }
}