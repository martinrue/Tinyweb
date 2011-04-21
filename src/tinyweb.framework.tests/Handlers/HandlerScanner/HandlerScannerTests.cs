using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HandlerScannerTests
    {
        [TearDown]
        public void TearDown()
        {
            HandlerScanner.SetHandlerScanner(new DefaultHandlerScanner());
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultHandlerScanner()
        {
            Assert.IsInstanceOf<DefaultHandlerScanner>(HandlerScanner.Current);
        }

        [Test]
        public void Current_WhenSetToCustomHandlerScanner_ReturnsCustomHandlerScanner()
        {
            HandlerScanner.SetHandlerScanner(new CustomHandlerScanner());
            Assert.IsInstanceOf<CustomHandlerScanner>(HandlerScanner.Current);
        }
    }
}