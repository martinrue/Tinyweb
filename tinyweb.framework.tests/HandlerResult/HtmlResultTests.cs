using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class HtmlResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNewString_ReturnsString()
        {
            var result = new HtmlResult("test data");
            Assert.That(result.GetResult(), Is.EqualTo("test data"));
        }

        [Test]
        public void GetResult_WhenCreated_ContentTypeIsTextHtml()
        {
            var result = new HtmlResult("test data");
            Assert.That(result.ContentType, Is.EqualTo("text/html"));
        }

        [Test]
        public void GetResult_WhenCreatedWithImplicitConversion_ReturnsString()
        {
            HtmlResult result = "test data";
            Assert.That(result.GetResult(), Is.EqualTo("test data"));
        }
    }
}