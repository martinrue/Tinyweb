using System.IO;
using NUnit.Framework;
using tinyweb.viewengine.spark;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class SparkResultTests
    {
        [Test]
        public void GetResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new SparkResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The spark view at c:\\fakepath could not be found"));
        }

        [Test]
        public void GetResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var result = new SparkResult("..\\..\\Test Data\\Views\\Spark\\Simple.spark");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<h1>header</h1>"));
        }

        [Test]
        public void GetResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var model = new UserModel { ID = 42, Username = "Username" };
            var result = new SparkResult<UserModel>(model, "..\\..\\Test Data\\Views\\Spark\\View.spark");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<h1>42</h1>"));
            Assert.That(result.GetResult(), Contains.Substring("<h2>Username</h2>"));
        }

        [Test]
        public void GetResult_WhenRequestedWithModelUsingPartial_ReturnsCombinedView()
        {
            var model = new UserModel { ID = 42, Username = "Username" };
            var result = new SparkResult<UserModel>(model, "..\\..\\Test Data\\Views\\Spark\\View.spark");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<span>partial</span>"));
        }

        [Test]
        public void GetResult_WhenRequestedWithModelUsingMaster_ReturnsCombinedView()
        {
            var result = new SparkResult("..\\..\\Test Data\\Views\\Spark\\Child.spark", "Master.spark");

            Assert.That(result.ContentType, Is.EqualTo("text/html"));
            Assert.That(result.GetResult(), Contains.Substring("<h1>hello world</h1>"));
        }
    }
}