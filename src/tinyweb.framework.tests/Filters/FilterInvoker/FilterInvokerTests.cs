using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class FilterInvokerTests
    {
        [TearDown]
        public void TearDown()
        {
            FilterInvoker.SetFilterInvoker(new DefaultFilterInvoker(new ArgumentBuilder()));
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultFilterInvoker()
        {
            Assert.IsInstanceOf<DefaultFilterInvoker>(FilterInvoker.Current);
        }

        [Test]
        public void Current_WhenSetToCustomFilterInvoker_ReturnsCustomFilterInvoker()
        {
            FilterInvoker.SetFilterInvoker(new CustomFilterInvoker());
            Assert.IsInstanceOf<CustomFilterInvoker>(FilterInvoker.Current);
        }
    }
}