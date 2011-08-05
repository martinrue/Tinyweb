using System.IO;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HtmlResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new HtmlResult("c:\\fakepath").ProcessResult(null, null));
            Assert.That(exception.Message, Is.EqualTo("The view at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithExistingPath_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var result = new HtmlResult("..\\..\\Test Data\\HandlerResult\\Views\\View.html");

            Assert.That(result.FilePath, Is.EqualTo("..\\..\\Test Data\\HandlerResult\\Views\\View.html"));

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Is.EqualTo("<h1>View</h1>"));
        }
    }
}