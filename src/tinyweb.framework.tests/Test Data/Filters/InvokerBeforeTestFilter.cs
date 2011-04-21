namespace tinyweb.framework.tests
{
    public class InvokerBeforeTestFilter
    {
        public int Number { get; set; }

        public IHandlerResult Before(int number)
        {
            Number = number;
            return new NoneResult();
        }
    }
}