namespace tinyweb.framework.tests
{
    [RunPriority(1)]
    public class AfterFilter
    {
        public IHandlerResult After()
        {
            return new NoneResult();
        }
    }
}