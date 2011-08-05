using System;
using NUnit.Framework;
using tinyweb.framework.Helpers;

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
        public void ProcessResult_WhenCreatedWithValidHandler_ReturnsHandlerUri()
        {
            Url.ApplicationPathProvider = new FakeApplicationPathProvider();

            Tinyweb.Handlers = new[] { new HandlerData { Type = typeof(Resource1Handler), Uri = "uri" } };

            var response = new FakeResponseContext();
            var result = new RedirectResult<Resource1Handler>();

            Assert.That(result.Uri, Is.EqualTo("/uri"));

            result.ProcessResult(null, response);

            Assert.That(response.RedirectUrl, Is.EqualTo("/uri"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithValidUri_ReturnsUri()
        {
            var response = new FakeResponseContext();
            var result = new RedirectResult("someuri");

            Assert.That(result.Uri, Is.EqualTo("someuri"));

            result.ProcessResult(null, response);

            Assert.That(response.RedirectUrl, Is.EqualTo("someuri"));
        }
    }
}