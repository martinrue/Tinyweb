namespace tinyweb.framework
{
    public interface IRequestContext
    {
        IRequestHeaders Headers { get; }
        IRouteValues RouteValues { get; }
    }
}