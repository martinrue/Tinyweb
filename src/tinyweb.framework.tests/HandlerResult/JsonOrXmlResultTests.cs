using System;
using Moq;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class JsonOrXmlResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonOrXmlResult(null));
        }

        [Test]
        public void ProcessResults_WithNoAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept(String.Empty);
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithWildcardAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("*/*");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithImplicitEqualPriorityXmlAndJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml,application/json");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithExplicitEqualPriorityXmlAndJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.5,application/json;q=0.5");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithHigherPriorityJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.5,application/json;q=0.6");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithHigherPriorityXmlAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.7,application/json;q=0.6");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/xml"));
        }

        private FakeRequestContext buildFakeRequestWithAccept(string acceptHeader)
        {
            var headers = new Mock<IRequestHeaders>();
            headers.Setup(x => x[It.IsAny<string>()]).Returns(acceptHeader);
            
            return new FakeRequestContext(headers.Object);
        }
    }
}