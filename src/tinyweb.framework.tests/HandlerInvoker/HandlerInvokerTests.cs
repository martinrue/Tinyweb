using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HandlerInvokerTests
    {
        [TearDown]
        public void TearDown()
        {
            HandlerInvoker.SetHandlerInvoker(new DefaultHandlerInvoker());
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultHandlerInvoker()
        {
            Assert.IsInstanceOf<DefaultHandlerInvoker>(HandlerInvoker.Current);
        }

        [Test]
        public void Current_WhenSetToCustomHandlerInvoker_ReturnsCustomHandlerScanner()
        {
            HandlerInvoker.SetHandlerInvoker(new CustomHandlerInvoker());
            Assert.IsInstanceOf<CustomHandlerInvoker>(HandlerInvoker.Current);
        }
    }
}