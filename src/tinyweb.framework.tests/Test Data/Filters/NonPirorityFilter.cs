namespace tinyweb.framework.tests
{
    public class NonPriorityFilter
    {
        public IResult After()
        {
            return new NoneResult();
        }
    }
}