namespace tinyweb.framework
{
    public interface IRequestHeaders
    {
        string this[string header] { get; }
    }
}