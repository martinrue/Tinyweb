using System;
using tinyweb.framework.Helpers;

namespace tinyweb.framework
{
    public class RedirectResult<T> : IResult
    {
        public string Uri { get; set; }

        public RedirectResult(object arguments = null)
        {
            Uri = Url.For<T>(arguments);
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Redirect(Uri);
        }
    }

    public class RedirectResult : IResult
    {
        public string Uri { get; set; }

        public RedirectResult(string uri)
        {
            if (uri.IsEmpty())
            {
                throw new Exception("The specified redirect uri is invalid");
            }

            Uri = uri;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Redirect(Uri);
        }
    }
}