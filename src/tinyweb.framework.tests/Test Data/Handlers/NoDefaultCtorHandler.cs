namespace tinyweb.framework.tests
{
    public class NoDefaultCtorHandler
    {
        Route route = new Route("nodefaultctor");

        public NoDefaultCtorHandler(int required)
        {

        }
    }
}