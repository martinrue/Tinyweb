namespace tinyweb.framework.tests
{
    public class InvalidFilter
    {
        public IHandlerResult Unrecognised()
        {
            return new NoneResult();
        }
    }
}