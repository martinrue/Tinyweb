namespace tinyweb.framework.tests
{
    public class NoDefaultCtorFilter
    {
        public NoDefaultCtorFilter(int argument)
        {
            
        }

        public IResult After()
        {
            return new NoneResult();
        }
    }
}