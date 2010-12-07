using System.Collections.Generic;

namespace tinyweb.framework
{
    public class StringResult : IHandlerResult
    {
        string data;

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public bool IsFileResult
        {
            get { return false; }
        }

        public StringResult(string data)
        {
            this.data = data;
        }

        public static implicit operator StringResult(string input)
        {
            return new StringResult(input);
        }

        public string GetResult()
        {
            return data;
        }
    }
}