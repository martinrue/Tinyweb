using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class FilterScannerTests
    {
        [TearDown]
        public void TearDown()
        {
            FilterScanner.SetFilterScanner(new DefaultFilterScanner());
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultFilterScanner()
        {
            Assert.IsInstanceOf<DefaultFilterScanner>(FilterScanner.Current);
        }

        [Test]
        public void Current_WhenSetToCustomFilterScanner_ReturnsCustomFilterScanner()
        {
            FilterScanner.SetFilterScanner(new CustomFilterScanner());
            Assert.IsInstanceOf<CustomFilterScanner>(FilterScanner.Current);
        }
    }
}