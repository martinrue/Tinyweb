using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HandlerFactoryTests
    {
        [TearDown]
        public void TearDown()
        {
            HandlerFactory.SetHandlerFactory(new StructureMapHandlerFactory());
        }

        [Test]
        public void Current_WhenRequested_ReturnsDefaultHandlerFactory()
        {
            Assert.IsInstanceOf<ActivatorHandlerFactory>(HandlerFactory.Current);
        }

        [Test]
        public void Current_WhenSetToCustomHandlerFactory_ReturnsCustomHandlerFactory()
        {
            HandlerFactory.SetHandlerFactory(new CustomHandlerFactory());
            Assert.IsInstanceOf<CustomHandlerFactory>(HandlerFactory.Current);
        }
    }
}