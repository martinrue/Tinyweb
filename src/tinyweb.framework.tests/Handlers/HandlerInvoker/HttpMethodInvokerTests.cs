using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web.Routing;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HttpMethodInvokerTests
    {
        IHandlerInvoker defaultHandlerInvoker;
        RequestContext requestContext;

        [SetUp]
        public void Setup()
        {
            defaultHandlerInvoker = new DefaultHandlerInvoker(new ArgumentBuilder());
            requestContext = new RequestContext { RouteData = new RouteData() };
        }

        [Test]
        public void HttpGET_WithQueryStringParameters_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext(queryString: new NameValueCollection { {"param1", "1"}, {"param2", "world"} });
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("1world"));
        }

        [Test]
        public void HttpGET_WithFormPostParameters_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext(formData: new NameValueCollection { { "param1", "5" }, { "param2", "times" } });
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("5times"));
        }

        [Test]
        public void HttpGET_WithRouteDataParameters_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext();
            requestContext.RouteData.Values.Add("param1", "10");
            requestContext.RouteData.Values.Add("param2", "strings");
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("10strings"));
        }

        [Test]
        public void HttpPOST_WithAllParameterSources_CallsHandlerPostMethodAndReturnsCorrectData()
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext(queryString: new NameValueCollection { { "param1", "42" } }, formData: new NameValueCollection { {"param2", "1.141"} });
            requestContext.RouteData.Values.Add("param3", "true");
            requestContext.HttpContext.Request.SetHttpMethodResult("POST");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("421.141True"));

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Test]
        public void HttpPUT_WithSimpleModelParameter_CallsHandlerPutMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext(queryString: new NameValueCollection { { "number1", "4" }, { "number2", "5" } });
            requestContext.HttpContext.Request.SetHttpMethodResult("PUT");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("Result: 9"));
        }

        [Test]
        public void HttpDELETE_WithComplexModelParameter_CallsHandlerDeleteMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext(queryString: new NameValueCollection { { "number1", "13" }, { "number2", "11" } }, formData: new NameValueCollection { { "factor", "10" } });
            requestContext.RouteData.Values.Add("label", "the total is");
            requestContext.HttpContext.Request.SetHttpMethodResult("DELETE");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new InvokeHandler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(result.Result, Is.InstanceOf<StringResult>());
            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("the total is 240"));
        }
    }
}