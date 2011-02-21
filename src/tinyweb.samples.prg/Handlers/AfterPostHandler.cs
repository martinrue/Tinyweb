using tinyweb.framework;

namespace tinyweb.samples.prg.Handlers
{
    public class AfterPostHandler
    {
        public IHandlerResult Get()
        {
            return Result.String("After Post and Redirect");
        }
    }
}