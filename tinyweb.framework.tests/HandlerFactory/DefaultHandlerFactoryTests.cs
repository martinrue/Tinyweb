using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultHandlerFactoryTests
    {
        IHandlerFactory defaultHandlerFactory;

        [SetUp]
        public void Setup()
        {
            defaultHandlerFactory = new DefaultHandlerFactory();
        }

        [Test]
        public void Create_WithResource1HandlerRequest_CreatesInstanceOfResource1Handler()
        {
            var handler = defaultHandlerFactory.Create(new HandlerData { Type = typeof(Resource1Handler) });

            Assert.IsInstanceOf<Resource1Handler>(handler);
        }

        [Test]
        public void Create_WithResource2HandlerRequest_CreatesInstanceOfResource2Handler()
        {
            var handler = defaultHandlerFactory.Create(new HandlerData { Type = typeof(Resource2Handler) });

            Assert.IsInstanceOf<Resource2Handler>(handler);
        }

        [Test]
        public void Create_WithResource3HandlerRequest_CreatesInstanceOfResource3Handler()
        {
            var handler = defaultHandlerFactory.Create(new HandlerData { Type = typeof(Resource3Handler) });

            Assert.IsInstanceOf<Resource3Handler>(handler);
        }

        [Test]
        public void Create_WithNoDefaultCtorHandlerRequest_ThrowsNoParameterlessConstructorException()
        {
            var exception = Assert.Throws<NoParameterlessConstructorException>(() => 
            {
                defaultHandlerFactory.Create(new HandlerData { Type = typeof(NoDefaultCtorHandler) });
            });

            StringAssert.AreEqualIgnoringCase("No parameterless constructor found for type tinyweb.framework.tests.NoDefaultCtorHandler", exception.Message);
        }
    }
}