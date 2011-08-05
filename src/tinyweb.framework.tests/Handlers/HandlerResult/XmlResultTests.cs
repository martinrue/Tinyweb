using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class XmlResultTests
    {
        [Test]
        public void ProcessResult_WhenCreatedWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlResult(null));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithCustomObject_ReturnsCorrectXmlRepresentation()
        {
            var response = new FakeResponseContext();
            var result = new XmlResult(new CustomType { Data = "data", Number = 50 });

            Assert.That((result.Data as CustomType).Data, Is.EqualTo("data"));
            Assert.That((result.Data as CustomType).Number, Is.EqualTo(50));

            result.ProcessResult(null, response);

            var expected = "";
            expected += "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n";
            expected += "<CustomType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n";
            expected += "  <Data>data</Data>\r\n";
            expected += "  <Number>50</Number>\r\n";
            expected += "</CustomType>";

            Assert.That(response.ContentType, Is.EqualTo("application/xml"));
            Assert.That(response.Response, Is.EqualTo(expected));
        }

        [Test]
        public void ProcessResult_WhenCreatedWithCollection_ReturnsCorrectXmlRepresentation()
        {
            var response = new FakeResponseContext();
            var result = new XmlResult(new List<CustomType> { new CustomType { Data = "data1", Number = 1 }, new CustomType { Data = "data2", Number = 2 } });

            Assert.That(result.Data as List<CustomType>, Is.Not.Empty);

            result.ProcessResult(null, response);

            var expected = "";
            expected += "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n";
            expected += "<ArrayOfCustomType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n";
            expected += "  <CustomType>\r\n";
            expected += "    <Data>data1</Data>\r\n";
            expected += "    <Number>1</Number>\r\n";
            expected += "  </CustomType>\r\n";
            expected += "  <CustomType>\r\n";
            expected += "    <Data>data2</Data>\r\n";
            expected += "    <Number>2</Number>\r\n";
            expected += "  </CustomType>\r\n";
            expected += "</ArrayOfCustomType>";

            Assert.That(response.ContentType, Is.EqualTo("application/xml"));
            Assert.That(response.Response, Is.EqualTo(expected));
        }
    }
}