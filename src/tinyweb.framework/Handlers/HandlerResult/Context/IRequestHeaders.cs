namespace tinyweb.framework
{
    public interface IRequestHeaders
    {
        string this[string header] { get; }
        bool KeyExists(string key);
    }
}