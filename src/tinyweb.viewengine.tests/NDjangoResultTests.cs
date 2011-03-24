using System.IO;
using NUnit.Framework;
using tinyweb.framework;
using tinyweb.viewengine.ndjango;

namespace tinyweb.viewengine.tests
{
    [TestFixture]
    public class NDjangoResultTests
    {
        [Test]
        public void ProcessResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new NDjangoResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The django view at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var result = new NDjangoResult("..\\..\\Test Data\\Views\\NDjango\\NoModel.django");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>content</h1>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var model = new UserModel {ID = 42, Username = "Username"};
            var result = new NDjangoResult<UserModel>(model, "..\\..\\Test Data\\Views\\NDjango\\Model.django");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("42Username"));
        }
    }
}