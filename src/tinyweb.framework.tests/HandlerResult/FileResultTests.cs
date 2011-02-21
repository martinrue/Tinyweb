using System.IO;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class FileResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new FileResult("c:\\fakepath"));
            Assert.That(exception.Message, Is.EqualTo("The file at c:\\fakepath could not be found"));
        }

        [Test]
        public void GetResult_WhenCreatedWithPathToHtmlFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Views\\View.html");
            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.IsTrue(result.GetResult().EndsWith("\\Test Data\\HandlerResult\\Views\\View.html"));
            Assert.That(result.CustomHeaders["Content-Disposition"], Is.EqualTo("attachment; filename=View.html"));
        }

        [Test]
        public void GetResult_WhenCreatedWithPathToPDFFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Files\\Download.txt");
            Assert.That(result.ContentType, Is.EqualTo("text/plain"));
            Assert.IsTrue(result.GetResult().EndsWith("\\Test Data\\HandlerResult\\Files\\Download.txt"));
            Assert.That(result.CustomHeaders["Content-Disposition"], Is.EqualTo("attachment; filename=Download.txt"));
        }

        [Test]
        public void GetResult_WhenCreatedWithPathToUnknownFile_ReturnsPathToFileAndHasDispositionHeader()
        {
            var result = new FileResult("..\\..\\Test Data\\HandlerResult\\Files\\Download.unknown");
            Assert.That(result.ContentType, Is.EqualTo("application/unknown"));
            Assert.IsTrue(result.GetResult().EndsWith("\\Test Data\\HandlerResult\\Files\\Download.unknown"));
            Assert.That(result.CustomHeaders["Content-Disposition"], Is.EqualTo("attachment; filename=Download.unknown"));
        }
    }
}