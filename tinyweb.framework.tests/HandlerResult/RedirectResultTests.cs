using System;
using NUnit.Framework;
using tinyweb.framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class RedirectResultTests
    {
        [Test]
        public void Ctor_WhenCreatedWithInvalidHandler_ThrowsException()
        {
            Tinyweb.Handlers = new[] { new HandlerData() };

            var exception = Assert.Throws<Exception>(() => new RedirectResult<InvalidHandler>());
            Assert.That(exception.Message, Is.EqualTo("The handler InvalidHandler was not found"));
        }

        [Test]
        public void Ctor_WhenCreatedWithInvalidUri_ThrowsException()
        {
            var exception = Assert.Throws<Exception>(() => new RedirectResult(String.Empty));
            Assert.That(exception.Message, Is.EqualTo("The specified redirect uri is invalid"));
        }

        [Test]
        public void GetResult_WhenCreatedWithValidHandler_ReturnsHandlerUri()
        {
            Tinyweb.Handlers = new[] { new HandlerData { Type = typeof(Resource1Handler), Uri = "uri" } };

            var result = new RedirectResult<Resource1Handler>();
            Assert.That(result.GetResult(), Is.EqualTo("uri"));
        }

        [Test]
        public void GetResult_WhenCreatedWithValidUri_ReturnsUri()
        {
            var result = new RedirectResult("someuri");
            Assert.That(result.GetResult(), Is.EqualTo("someuri"));
        }
    }
}