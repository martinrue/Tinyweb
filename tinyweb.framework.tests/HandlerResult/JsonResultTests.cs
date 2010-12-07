using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class JsonResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNull_ReturnsEmptyString()
        {
            var result = new JsonResult(null);
            Assert.That(result.GetResult(), Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetResult_WhenCreatedWithAnonymousObject_ReturnsCorrectJsonRepresentation()
        {
            var result = new JsonResult(new { message = "hello world", number = 42 });
            Assert.That(result.ContentType, Is.EqualTo("text/json"));
            Assert.That(result.GetResult(), Is.EqualTo("{\"message\":\"hello world\",\"number\":42}"));
        }

        [Test]
        public void GetResult_WhenCreatedWithCustomObject_ReturnsCorrectJsonRepresentation()
        {
            var result = new JsonResult(new CustomType { Data = "data", Number = 50 });
            Assert.That(result.ContentType, Is.EqualTo("text/json"));
            Assert.That(result.GetResult(), Is.EqualTo("{\"Data\":\"data\",\"Number\":50}"));
        }

        [Test]
        public void GetResult_WhenCreatedWithCollection_ReturnsCorrectJsonRepresentation()
        {
            var result = new JsonResult(new List<CustomType> { new CustomType { Data = "data1", Number = 1 }, new CustomType { Data = "data2", Number = 2 } });
            Assert.That(result.ContentType, Is.EqualTo("text/json"));
            Assert.That(result.GetResult(), Is.EqualTo("[{\"Data\":\"data1\",\"Number\":1},{\"Data\":\"data2\",\"Number\":2}]"));
        }
    }
}