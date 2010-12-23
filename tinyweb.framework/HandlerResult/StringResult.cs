using System.Collections.Generic;

namespace tinyweb.framework
{
    public class StringResult : IHandlerResult
    {
        string data;

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Render; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public StringResult(string data)
        {
            this.data = data;
        }

        public string GetResult()
        {
            return data;
        }
    }
}