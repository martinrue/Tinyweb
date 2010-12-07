using tinyweb.framework;

namespace tinyweb.sample.Handlers
{
    public class HomeHandler
    {
        public StringResult Get()
        {
            return "Hello World";
        }
    }
}