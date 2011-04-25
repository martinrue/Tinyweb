namespace tinyweb.framework.tests
{
    public class InvokerBeforeTestFilter
    {
        public int Number { get; set; }

        public IResult Before(int number)
        {
            Number = number;
            return new NoneResult();
        }
    }
}