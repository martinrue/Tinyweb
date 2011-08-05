using System;
using System.Web.Routing;
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

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithWildcardAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("*/*");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithImplicitEqualPriorityXmlAndJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml,application/json");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithExplicitEqualPriorityXmlAndJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.5,application/json;q=0.5");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithHigherPriorityJsonAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.5,application/json;q=0.6");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithHigherPriorityXmlAcceptHeader_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAccept("application/xml;q=0.7,application/json;q=0.6");
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/xml"));
        }

        [Test]
        public void ProcessResults_WithXmlFormatExtensionOverride_ReturnsXmlResult()
        {
            var request = buildFakeRequestWithAcceptAndRouteValues("application/xml;q=0.7,application/json;q=0.9", new RouteValueDictionary { {"format", "xml"} });
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/xml"));
        }

        [Test]
        public void ProcessResults_WithJsonFormatExtensionOverride_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAcceptAndRouteValues("application/xml;q=0.7,application/json;q=0.1", new RouteValueDictionary { { "format", "json" } });
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ProcessResults_WithUnknownFormatExtensionOverride_ReturnsJsonResult()
        {
            var request = buildFakeRequestWithAcceptAndRouteValues("application/xml;q=0.7,application/json;q=0.1", new RouteValueDictionary { { "format", "unknown" } });
            var response = new FakeResponseContext();

            var result = new JsonOrXmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(request, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        private FakeRequestContext buildFakeRequestWithAccept(string acceptHeader)
        {
            var headers = new Mock<IRequestHeaders>();
            headers.Setup(x => x[It.IsAny<string>()]).Returns(acceptHeader);
            headers.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);

            var routeValues = new RouteValues(new RouteValueDictionary());
            
            return new FakeRequestContext(headers.Object, routeValues);
        }

        private FakeRequestContext buildFakeRequestWithAcceptAndRouteValues(string acceptHeader, RouteValueDictionary values)
        {
            var headers = new Mock<IRequestHeaders>();
            headers.Setup(x => x[It.IsAny<string>()]).Returns(acceptHeader);

            var routeValues = new RouteValues(values);

            return new FakeRequestContext(headers.Object, routeValues);
        }
    }
}