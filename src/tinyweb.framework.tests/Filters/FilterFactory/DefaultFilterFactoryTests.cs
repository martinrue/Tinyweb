using StructureMap;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultFilterFactoryTests
    {
        IFilterFactory defaultFilterFactory;

        [SetUp]
        public void Setup()
        {
            defaultFilterFactory = new DefaultFilterFactory();
        }

        [Test]
        public void Create_ForBeforeFilter_CreatesInstanceOfBeforeFilter()
        {
            var filter = defaultFilterFactory.Create(new FilterData { Type = typeof(BeforeFilter) });
            Assert.IsInstanceOf<BeforeFilter>(filter);
        }

        [Test]
        public void Create_ForAfterFilter_CreatesInstanceOfAfterFilter()
        {
            var filter = defaultFilterFactory.Create(new FilterData { Type = typeof(AfterFilter) });
            Assert.IsInstanceOf<AfterFilter>(filter);
        }

        [Test]
        public void Create_ForBeforeAndAfterFilter_CreatesInstanceOfBeforeAndAfterFilter()
        {
            var filter = defaultFilterFactory.Create(new FilterData { Type = typeof(BeforeAndAfterFilter) });
            Assert.IsInstanceOf<BeforeAndAfterFilter>(filter);
        }

        [Test]
        public void Create_WithNoDefaultCtorFilter_ThrowsNoParameterlessConstructorException()
        {
            Assert.Throws<StructureMapException>(() =>
            {
                defaultFilterFactory.Create(new FilterData { Type = typeof(NoDefaultCtorFilter) });
            });
        }
    }
}