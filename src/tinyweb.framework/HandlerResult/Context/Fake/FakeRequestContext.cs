namespace tinyweb.framework
{
    public class FakeRequestContext : IRequestContext
    {
        public IRequestHeaders Headers { get; private set; }
        public IRouteValues RouteValues { get; private set; }

        public FakeRequestContext(IRequestHeaders headers, IRouteValues routeValues)
        {
            Headers = headers;
            RouteValues = routeValues;
        }
    }
}