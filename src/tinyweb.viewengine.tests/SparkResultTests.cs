using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using tinyweb.framework;
using tinyweb.viewengine.spark;

namespace tinyweb.viewengine.tests
{
    [TestFixture]
    public class SparkResultTests
    {
        [Test]
        public void ProcessResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new SparkResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The spark view at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var result = new SparkResult("../../Test Data/Views/Spark/Simple.spark");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>header</h1>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var model = new UserModel { ID = 42, Username = "Username" };
            var result = new SparkResult<UserModel>(model, "../../Test Data/Views/Spark/View.spark");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>42</h1>"));
            Assert.That(response.Response, Contains.Substring("<h2>Username</h2>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModelUsingPartial_ReturnsCombinedView()
        {
            var response = new FakeResponseContext();
            var model = new UserModel { ID = 42, Username = "Username" };
            var result = new SparkResult<UserModel>(model, "../../Test Data/Views/Spark/View.spark");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<span>partial</span>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModelUsingMaster_ReturnsCombinedView()
        {
            var response = new FakeResponseContext();
            var result = new SparkResult("../../Test Data/Views/Spark/Child.spark", "Master.spark");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>hello world</h1>"));
        }

        [Test]
        public void ProcessResult_WhenOutputtingDateTime_IsAbleToAccessSystemNamespace()
        {
            var response = new FakeResponseContext();
            var result = new SparkResult("../../Test Data/Views/Spark/DateTime.spark");
            
            result.ProcessResult(null, response);

            DateTime date;

            Assert.That(DateTime.TryParse(response.Response, out date));
        }

        [Test]
        public void ProcessResult_WhenOutputtingHandlerUrl_IsAbleToAccessTinywebHandlersNamespace()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "foo/bar", Type = typeof(TestHandler) }
            };

            var response = new FakeResponseContext();
            var result = new SparkResult("../../Test Data/Views/Spark/Url.spark");

            result.ProcessResult(null, response);

            Assert.That(response.Response, Is.EqualTo("/foo/bar?key=value"));
        }
    }
}