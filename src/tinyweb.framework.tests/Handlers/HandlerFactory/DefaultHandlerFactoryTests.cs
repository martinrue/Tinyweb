﻿using StructureMap;
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
            defaultHandlerFactory = new StructureMapHandlerFactory();
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
            Assert.Throws<StructureMapException>(() => 
            {
                defaultHandlerFactory.Create(new HandlerData { Type = typeof(NoDefaultCtorHandler) });
            });
        }
    }
}