using System.IO;
using NUnit.Framework;
using tinyweb.framework;
using tinyweb.viewengine.nhaml;

namespace tinyweb.viewengine.tests
{
    [TestFixture]
    public class NHamlResultTests
    {
        [Test]
        public void ProcessResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new NHamlResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The haml view at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var result = new NHamlResult("..\\..\\Test Data\\Views\\NHaml\\View.haml");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>testing</h1>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var model = new UserModel {ID = 42, Username = "Username"};
            var result = new NHamlResult<UserModel>(model, "..\\..\\Test Data\\Views\\NHaml\\Hello.haml");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>"));
            Assert.That(response.Response, Contains.Substring("42"));
            Assert.That(response.Response, Contains.Substring("</h1>"));
            Assert.That(response.Response, Contains.Substring("<h2>"));
            Assert.That(response.Response, Contains.Substring("Username"));
            Assert.That(response.Response, Contains.Substring("</h2>"));
        }
    }
}