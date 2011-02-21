using System.IO;
using NUnit.Framework;
using tinyweb.viewengine.nhaml;

namespace tinyweb.viewengine.tests
{
    [TestFixture]
    public class NHamlResultTests
    {
        [Test]
        public void GetResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new NHamlResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The haml view at c:\\fakepath could not be found"));
        }

        [Test]
        public void GetResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var result = new NHamlResult("..\\..\\Test Data\\Views\\NHaml\\View.haml");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<h1>testing</h1>"));
        }

        [Test]
        public void GetResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var model = new UserModel {ID = 42, Username = "Username"};
            var result = new NHamlResult<UserModel>(model, "..\\..\\Test Data\\Views\\NHaml\\Hello.haml");
            var output = result.GetResult();

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(output, Contains.Substring("<h1>"));
            Assert.That(output, Contains.Substring("42"));
            Assert.That(output, Contains.Substring("</h1>"));
            Assert.That(output, Contains.Substring("<h2>"));
            Assert.That(output, Contains.Substring("Username"));
            Assert.That(output, Contains.Substring("</h2>"));
        }
    }
}