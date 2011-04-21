namespace tinyweb.framework.tests
{
    public class NoDefaultCtorFilter
    {
        public NoDefaultCtorFilter(int argument)
        {
            
        }

        public IHandlerResult After()
        {
            return new NoneResult();
        }
    }
}