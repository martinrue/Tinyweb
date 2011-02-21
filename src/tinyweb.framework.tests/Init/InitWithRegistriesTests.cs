using NUnit.Framework;
using StructureMap;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class InitWithRegistriesTests
    {
        [SetUp]
        public void Setup()
        {
            Tinyweb.Init(new FooRegistry(), new BarRegistry());
        }

        [Test]
        public void Initialise_WithTwoRegistries_ContainsTwoRegistries()
        {
            var fooRepo = ObjectFactory.GetInstance<IFooRepository>();
            var barRepo = ObjectFactory.GetInstance<IBarRepository>();

            Assert.That(fooRepo, Is.Not.Null);
            Assert.That(fooRepo, Is.InstanceOf<FooRepository>());
            Assert.That(barRepo, Is.Not.Null);
            Assert.That(barRepo, Is.InstanceOf<BarRepository>());
        }
    }
}
