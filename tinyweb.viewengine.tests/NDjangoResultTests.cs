using System.IO;
using NUnit.Framework;
using tinyweb.viewengine.ndjango;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class NDjangoResultTests
    {
        [Test]
        public void GetResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new NDjangoResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The django view at c:\\fakepath could not be found"));
        }

        [Test]
        public void GetResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var result = new NDjangoResult("..\\..\\Test Data\\Views\\NDjango\\NoModel.django");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<h1>content</h1>"));
        }

        [Test]
        public void GetResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var model = new UserModel {ID = 42, Username = "Username"};
            var result = new NDjangoResult<UserModel>(model, "..\\..\\Test Data\\Views\\NDjango\\Model.django");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("42Username"));
        }
    }
}