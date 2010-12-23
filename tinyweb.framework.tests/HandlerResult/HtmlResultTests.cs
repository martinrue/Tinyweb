using System.IO;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HtmlResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new HtmlResult("c:\\fakepath"));
            Assert.That(exception.Message, Is.EqualTo("The view at c:\\fakepath could not be found"));
        }

        [Test]
        public void GetResult_WhenCreatedWithExistingPath_ReturnsViewData()
        {
            var result = new HtmlResult("..\\..\\Test Data\\HandlerResult\\Views\\View.html");
            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Is.EqualTo("<h1>View</h1>"));
        }
    }
}