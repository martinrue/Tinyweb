using System;
using tinyweb.framework.Helpers;

namespace tinyweb.framework
{
    public class RedirectResult<T> : IResult
    {
        string _uri;

        public RedirectResult(object arguments = null)
        {
            _uri = Url.For<T>(arguments);
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Redirect(_uri);
        }
    }

    public class RedirectResult : IResult
    {
        string _uri;

        public RedirectResult(string uri)
        {
            if (uri.IsEmpty())
            {
                throw new Exception("The specified redirect uri is invalid");
            }

            _uri = uri;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Redirect(_uri);
        }
    }
}