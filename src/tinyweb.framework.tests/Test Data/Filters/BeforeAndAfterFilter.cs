namespace tinyweb.framework.tests
{
    [FilterPriority(2)]
    public class BeforeAndAfterFilter
    {
        public IResult After()
        {
            return new NoneResult();
        }

        public IResult Before()
        {
            return new NoneResult();
        }
    }
}