using System.Collections.Generic;

namespace tinyweb.framework
{
    public class FakeResponseContext : IResponseContext
    {
        public Dictionary<string, string> Headers
        {
            get; set;
        }

        public string Response
        {
            get; set;
        }

        public string RedirectUrl
        {
            get; set;
        }

        public FakeResponseContext()
        {
            Headers = new Dictionary<string, string>();
        }

        public string ContentType
        {
            get; set;
        }

        public void AddHeader(string name, string value)
        {
            Headers.Add(name, value);
        }

        public void Write(string data)
        {
            Response = data;
        }

        public void WriteFile(string data)
        {
            Response = data;
        }

        public void Redirect(string url)
        {
            RedirectUrl = url;
        }
    }
}