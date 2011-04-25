namespace tinyweb.framework.tests
{
    public class InvokerAfterTestFilter
    {
        public string Phrase { get; set; }

        public IResult After(string phrase)
        {
            Phrase = phrase;
            return new NoneResult();
        }
    }
}