namespace tinyweb.framework
{
    public interface IHandlerResult
    {
        string ContentType { get; }
        string GetResult();
    }
}