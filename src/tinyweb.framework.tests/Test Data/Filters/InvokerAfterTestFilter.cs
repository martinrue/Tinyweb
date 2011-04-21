namespace tinyweb.framework.tests
{
    public class InvokerAfterTestFilter
    {
        public string Phrase { get; set; }

        public IHandlerResult After(string phrase)
        {
            Phrase = phrase;
            return new NoneResult();
        }
    }
}