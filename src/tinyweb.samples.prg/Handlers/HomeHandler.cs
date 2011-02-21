using tinyweb.framework;

namespace tinyweb.samples.prg.Handlers
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            return Result.Html("Views/Post.html");
        }

        public IHandlerResult Post()
        {
            return Result.Redirect<AfterPostHandler>();
        }
    }
}