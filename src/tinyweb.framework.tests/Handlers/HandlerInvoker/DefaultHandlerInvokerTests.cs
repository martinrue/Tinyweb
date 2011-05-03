using System.Web;
using System.Web.Routing;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultHandlerInvokerTests
    {
        IHandlerInvoker defaultHandlerInvoker;
        RequestContext requestContext;

        [SetUp]
        public void Setup()
        {
            defaultHandlerInvoker = new DefaultHandlerInvoker(new ArgumentBuilder());
            requestContext = new RequestContext { HttpContext = MvcMockHelpers.FakeHttpContext() };
        }

        [Test]
        public void Execute_WithHttpGETOnResource1Handler_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("Get"));
        }

        [Test]
        public void Execute_WithHttpPOSTOnResource1Handler_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("POST");

            var response = new FakeResponseContext();
            var result = defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext, null);

            result.Result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("Post"));
        }

        [Test]
        public void Execute_WithHttpDELETEOnResource1Handler_ThrowsHttp501Error()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("DELETE");

            var exception = Assert.Throws<HttpException>(() => defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext, null));
            Assert.That(exception.Message, Is.EqualTo("The request could not be completed because the resource does not support DELETE"));
        }

        [Test]
        public void Execute_WithBeforeAndAfterMethod_CallsBeforeAndThenAfter()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");
            
            var handler = new BeforeAfterHandler();

            Assert.That(handler.Calls.Count, Is.EqualTo(0));

            defaultHandlerInvoker.Execute(handler, requestContext, null);

            Assert.That(handler.Calls[0], Is.EqualTo("before"));
            Assert.That(handler.Calls[1], Is.EqualTo("get"));
            Assert.That(handler.Calls[2], Is.EqualTo("after"));
        }
    }
}