using System;
using System.Collections.Generic;
using System.Linq;

namespace tinyweb.framework
{
    public class RedirectResult<T> : IHandlerResult
    {
        string handlerUri;

        public RedirectResult()
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(T));

            if (handler == null)
            {
                throw new Exception("The handler {0} was not found".With(typeof(T).Name));
            }

            // todo: the handler uri could have route value placeholders and need to be replaced
            handlerUri = handler.Uri;
        }

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Redirect; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }
    
        public string GetResult()
        {
            return handlerUri;
        }
    }

    public class RedirectResult : IHandlerResult
    {
        string uri;

        public RedirectResult(string uri)
        {
            if (uri.IsEmpty())
            {
                throw new Exception("The specified redirect uri is invalid");
            }

            this.uri = uri;
        }

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Redirect; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public string GetResult()
        {
            return uri;
        }
    }
}