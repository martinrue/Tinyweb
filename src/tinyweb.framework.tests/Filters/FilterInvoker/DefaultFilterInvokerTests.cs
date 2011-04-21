using System.Web.Routing;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultFilterInvokerTests
    {
        IFilterInvoker filterInvoker;
        RequestContext requestContext;

        [SetUp]
        public void Setup()
        {
            filterInvoker = new DefaultFilterInvoker(new ArgumentBuilder());
            requestContext = new RequestContext { HttpContext = MvcMockHelpers.FakeHttpContext() };
        }

        [Test]
        public void BeforeFilter_WhenInvoked_CallsCorrectMethod()
        {
            requestContext.HttpContext.Request.QueryString.Add("number", "42");

            var filter = new InvokerBeforeTestFilter();
            filterInvoker.RunBefore(filter, requestContext);

            Assert.That(filter.Number, Is.EqualTo(42));
        }

        [Test]
        public void AfterFilter_WhenInvoked_CallsCorrectMethod()
        {
            requestContext.HttpContext.Request.QueryString.Add("phrase", "testing");

            var filter = new InvokerAfterTestFilter();
            filterInvoker.RunAfter(filter, requestContext);

            Assert.That(filter.Phrase, Is.EqualTo("testing"));
        }
    }
}