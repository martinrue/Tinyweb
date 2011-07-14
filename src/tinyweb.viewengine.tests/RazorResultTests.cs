using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using tinyweb.framework;
using tinyweb.framework.Helpers;
using tinyweb.viewengine.razor;

namespace tinyweb.viewengine.tests
{
    [TestFixture]
    public class RazorResultTests
    {
        [Test]
        public void ProcessResult_WhenRequestedWithNonExistentPath_ThrowsFileNotFoundException()
        {
            var exception = Assert.Throws<FileNotFoundException>(() => new RazorResult("c:\\fakepath"));

            Assert.That(exception.Message, Is.EqualTo("The razor view at c:\\fakepath could not be found"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithNoModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var result = new RazorResult("../../Test Data/Views/Razor/Simple.cshtml");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>header</h1>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModel_ReturnsViewData()
        {
            var response = new FakeResponseContext();
            var model = new UserModel { ID = 42, Username = "Username" };
            var result = new RazorResult<UserModel>(model, "../../Test Data/Views/Razor/View.cshtml");

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
            var result = new RazorResult<UserModel>(model, "../../Test Data/Views/Razor/View.cshtml");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<span>partial</span>"));
        }

        [Test]
        public void ProcessResult_WhenRequestedWithModelUsingMaster_ReturnsCombinedView()
        {
            var response = new FakeResponseContext();
            var result = new RazorResult("../../Test Data/Views/Razor/Child.cshtml", "Master.cshtml");

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
            Assert.That(response.Response, Contains.Substring("<h1>hello world</h1>"));
        }

        [Test]
        public void ProcessResult_WhenOutputtingDateTime_IsAbleToAccessSystemNamespace()
        {
            var response = new FakeResponseContext();
            var result = new RazorResult("../../Test Data/Views/Razor/DateTime.cshtml");

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

            Url.ApplicationPathProvider = new FakeApplicationPathProvider();

            var response = new FakeResponseContext();
            var result = new RazorResult("../../Test Data/Views/Razor/Url.cshtml");

            result.ProcessResult(null, response);

            Assert.That(response.Response, Is.EqualTo("/foo/bar?key=value"));
        }
    }
}
