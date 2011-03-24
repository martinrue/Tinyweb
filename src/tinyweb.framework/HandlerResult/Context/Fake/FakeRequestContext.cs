namespace tinyweb.framework
{
    public class FakeRequestContext : IRequestContext
    {
        public IRequestHeaders Headers { get; private set; }

        public FakeRequestContext(IRequestHeaders headers)
        {
            Headers = headers;
        }
    }
}