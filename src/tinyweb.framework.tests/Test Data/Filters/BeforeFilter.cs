namespace tinyweb.framework.tests
{
    [FilterPriority(3)]
    public class BeforeFilter
    {
        public IResult Before()
        {
            return new NoneResult();
        }
    }
}