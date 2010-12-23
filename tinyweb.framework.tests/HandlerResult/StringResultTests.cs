using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class StringResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNewString_ReturnsString()
        {
            var result = new StringResult("test data");
            Assert.That(result.GetResult(), Is.EqualTo("test data"));
        }

        [Test]
        public void GetResult_WhenCreated_ContentTypeIsTextHtml()
        {
            var result = new StringResult("test data");
            Assert.That(result.ContentType, Is.EqualTo("text/html"));
        }
    }
}