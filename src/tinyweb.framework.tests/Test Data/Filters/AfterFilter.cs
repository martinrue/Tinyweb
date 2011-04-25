namespace tinyweb.framework.tests
{
    [FilterPriority(1)]
    public class AfterFilter
    {
        public IResult After()
        {
            return new NoneResult();
        }
    }
}