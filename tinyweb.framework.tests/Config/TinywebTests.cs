using System.Linq;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class TinywebTests
    {
        [SetUp]
        public void Setup()
        {
            Tinyweb.Initialise();
        }

        [Test]
        public void Initialise_WithThreeHandlers_ReturnsCorrectNumberOfHandlers()
        {
            Assert.That(Tinyweb.Handlers.Count(), Is.EqualTo(4));
        }

        [Test]
        public void Initialise_WithResource1Handler_CorrectUriConfigured()
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(Resource1Handler));

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.Uri.ToLower(), Is.EqualTo("resource1"));
            Assert.That(handler.DefaultRouteValues.AsRouteValueDictionary()["route1"], Is.EqualTo("default1"));
            Assert.That(handler.DefaultRouteValues.AsRouteValueDictionary()["route2"], Is.EqualTo("default2"));
        }

        [Test]
        public void Initialise_WithResource2Handler_CorrectUriConfigured()
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(Resource2Handler));

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.Uri.ToLower(), Is.EqualTo("resource2"));
            Assert.That(handler.DefaultRouteValues, Is.Null);
        }

        [Test]
        public void Initialise_WithResource3Handler_CorrectUriConfigured()
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(Resource3Handler));

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.Uri.ToLower(), Is.EqualTo("resource3"));
            Assert.That(handler.DefaultRouteValues, Is.Null);
        }

        [Test]
        public void Initialise_WithNoDefaultCtorHandler_CorrectUriConfigured()
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(NoDefaultCtorHandler));

            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.Uri.ToLower(), Is.EqualTo("nodefaultctor"));
            Assert.That(handler.DefaultRouteValues, Is.Null);
        }
    }
}