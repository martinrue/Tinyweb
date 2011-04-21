using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class StringResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNewString_ReturnsString()
        {
            var response = new FakeResponseContext();
            var result = new StringResult("test data");

            result.ProcessResult(null, response);

            Assert.That(response.Response, Is.EqualTo("test data"));
        }

        [Test]
        public void ProcessResult_WhenCreated_ContentTypeIsTextHtml()
        {
            var response = new FakeResponseContext();
            var result = new StringResult("test data");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
        }
    }
}