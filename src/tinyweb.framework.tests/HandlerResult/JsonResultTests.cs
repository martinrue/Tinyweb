using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class JsonResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonResult(null));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithAnonymousObject_ReturnsCorrectJsonRepresentation()
        {
            var response = new FakeResponseContext();
            var result = new JsonResult(new { message = "hello world", number = 42 });

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
            Assert.That(response.Response, Is.EqualTo("{\"message\":\"hello world\",\"number\":42}"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithCustomObject_ReturnsCorrectJsonRepresentation()
        {
            var response = new FakeResponseContext();
            var result = new JsonResult(new CustomType { Data = "data", Number = 50 });

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
            Assert.That(response.Response, Is.EqualTo("{\"Data\":\"data\",\"Number\":50}"));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithCollection_ReturnsCorrectJsonRepresentation()
        {
            var response = new FakeResponseContext();
            var result = new JsonResult(new List<CustomType> { new CustomType { Data = "data1", Number = 1 }, new CustomType { Data = "data2", Number = 2 } });

            result.ProcessResult(null, response);

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
            Assert.That(response.Response, Is.EqualTo("[{\"Data\":\"data1\",\"Number\":1},{\"Data\":\"data2\",\"Number\":2}]"));
        }
    }
}