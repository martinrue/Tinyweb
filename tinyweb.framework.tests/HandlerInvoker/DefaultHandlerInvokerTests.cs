using System.Web;
using System.Web.Routing;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultHandlerInvokerTests2
    {
        IHandlerInvoker defaultHandlerInvoker;
        RequestContext requestContext;

        [SetUp]
        public void Setup()
        {
            defaultHandlerInvoker = new DefaultHandlerInvoker();

            requestContext = new RequestContext();
            requestContext.HttpContext = MvcMockHelpers.FakeHttpContext();
        }

        [Test]
        public void Execute_WithHttpGETOnResource1Handler_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("GET");

            var result = defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext);

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Is.EqualTo("Get"));
        }

        [Test]
        public void Execute_WithHttpPOSTOnResource1Handler_CallsHandlerGetMethodAndReturnsCorrectData()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("POST");

            var result = defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext);

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Is.EqualTo("Post"));
        }

        [Test]
        public void Execute_WithHttpDELETEOnResource1Handler_ThrowsHttp501Error()
        {
            requestContext.HttpContext.Request.SetHttpMethodResult("DELETE");

            var exception = Assert.Throws<HttpException>(() => defaultHandlerInvoker.Execute(new Resource1Handler(), requestContext));
            Assert.That(exception.Message, Is.EqualTo("The request could not be completed because the resource does not support DELETE"));
        }
    }
}