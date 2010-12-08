using tinyweb.framework;

namespace tinyweb.samples.structuremap
{
    public class HomeHandler
    {
        IHelloService helloService;

        public HomeHandler(IHelloService helloService)
        {
            this.helloService = helloService;
        }

        public IHandlerResult Get()
        {
            return Result.String(helloService.SayHello());
        }
    }
}