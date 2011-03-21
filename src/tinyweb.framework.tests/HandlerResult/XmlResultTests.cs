using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class XmlResultTests
    {
        [Test]
        public void GetResult_WhenCreatedWithNull_ReturnsEmptyString()
        {
            var result = new XmlResult(null);
            Assert.That(result.GetResult(), Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetResult_WhenCreatedWithCustomObject_ReturnsCorrectXmlRepresentation()
        {
            var result = new XmlResult(new CustomType { Data = "data", Number = 50 });
            
            var expected = "";
            expected += "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n";
            expected += "<CustomType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n";
            expected += "  <Data>data</Data>\r\n";
            expected += "  <Number>50</Number>\r\n";
            expected += "</CustomType>";

            Assert.That(result.ContentType, Is.EqualTo("text/xml"));
            Assert.That(result.GetResult(), Is.EqualTo(expected));
        }

        [Test]
        public void GetResult_WhenCreatedWithCollection_ReturnsCorrectXmlRepresentation()
        {
            var result = new XmlResult(new List<CustomType> { new CustomType { Data = "data1", Number = 1 }, new CustomType { Data = "data2", Number = 2 } });

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

            Assert.That(result.ContentType, Is.EqualTo("text/xml"));
            Assert.That(result.GetResult(), Is.EqualTo(expected));
        }
    }
}