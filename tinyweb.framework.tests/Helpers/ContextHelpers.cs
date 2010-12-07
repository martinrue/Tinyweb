using System;
using System.Collections.Specialized;
using System.Web;
using Moq;

namespace tinyweb.framework.tests
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext(NameValueCollection queryString = null, NameValueCollection formData = null)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(p => p.Request).Returns(request.Object);
            context.Setup(p => p.Response).Returns(response.Object);
            context.Setup(p => p.Session).Returns(session.Object);
            context.Setup(p => p.Server).Returns(server.Object);

            context.Setup(p => p.Request.QueryString).Returns(queryString != null ? new NameValueCollection(queryString) : new NameValueCollection());
            context.Setup(p => p.Request.Form).Returns(formData != null ? new NameValueCollection(formData) : new NameValueCollection());

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
            {
                return url.Substring(0, url.IndexOf("?"));
            }
            else
            {
                return url;
            }
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request).Setup(p => p.HttpMethod).Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (!url.StartsWith("~/"))
            {
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");
            }

            var mock = Mock.Get(request);

            mock.Setup(p => p.QueryString).Returns(GetQueryStringParameters(url));
            mock.Setup(p => p.AppRelativeCurrentExecutionFilePath).Returns(GetUrlFileName(url));
            mock.Setup(p => p.PathInfo).Returns(string.Empty);
        }
    }
}