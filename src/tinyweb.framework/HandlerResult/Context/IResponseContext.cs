namespace tinyweb.framework
{
    public interface IResponseContext
    {
        string ContentType { get; set; }
        void AddHeader(string name, string value);
        void Write(string data);
        void WriteFile(string data);
        void Redirect(string url);
    }
}