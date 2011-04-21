using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class FilterFactoryTests
    {
        [TearDown]
        public void TearDown()
        {
            FilterFactory.SetFilterFactory(new DefaultFilterFactory());
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultFilterFactory()
        {
            Assert.IsInstanceOf<DefaultFilterFactory>(FilterFactory.Current);
        }

        [Test]
        public void Current_WhenSetToCustomFilterFactory_ReturnsCustomFilterFactory()
        {
            FilterFactory.SetFilterFactory(new CustomFilterFactory());
            Assert.IsInstanceOf<CustomFilterFactory>(FilterFactory.Current);
        }
    }
}