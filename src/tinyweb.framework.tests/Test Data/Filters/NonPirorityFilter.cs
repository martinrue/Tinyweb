namespace tinyweb.framework.tests
{
    public class NonPriorityFilter
    {
        public IHandlerResult After()
        {
            return new NoneResult();
        }
    }
}