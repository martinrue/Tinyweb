using System.IO;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class FileResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new FileResult("c:\\fakepath"));
            Assert.That(exception.Message, Is.EqualTo("The file at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithPathToHtmlFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var response = new FakeResponseContext();
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Views\\View.html");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.IsTrue(response.Response.EndsWith("\\Test Data\\HandlerResult\\Views\\View.html"));
            Assert.That(response.Headers["Content-Disposition"], Is.EqualTo("attachment; filename=View.html"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithPathToPDFFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var response = new FakeResponseContext();
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Files\\Download.txt");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
            Assert.That(response.Response.EndsWith("\\Test Data\\HandlerResult\\Files\\Download.txt"));
            Assert.That(response.Headers["Content-Disposition"], Is.EqualTo("attachment; filename=Download.txt"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithPathToUnknownFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var response = new FakeResponseContext();
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Files\\Download.unknown");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("application/unknown"));
            Assert.IsTrue(response.Response.EndsWith("\\Test Data\\HandlerResult\\Files\\Download.unknown"));
            Assert.That(response.Headers["Content-Disposition"], Is.EqualTo("attachment; filename=Download.unknown"));
        }
    }
}