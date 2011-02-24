using System;
using System.Collections.Generic;
using tinyweb.framework.Helpers;

namespace tinyweb.framework
{
    public class RedirectResult<T> : IHandlerResult
    {
        string handlerUri;

        public RedirectResult(object arguments = null)
        {
            handlerUri = Url.For<T>(arguments);
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