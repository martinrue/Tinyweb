namespace tinyweb.framework.tests
{
    [RunPriority(2)]
    public class BeforeAndAfterFilter
    {
        public IHandlerResult After()
        {
            return new NoneResult();
        }

        public IHandlerResult Before()
        {
            return new NoneResult();
        }
    }
}