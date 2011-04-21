namespace tinyweb.framework.tests
{
    [RunPriority(3)]
    public class BeforeFilter
    {
        public IHandlerResult Before()
        {
            return new NoneResult();
        }
    }
}