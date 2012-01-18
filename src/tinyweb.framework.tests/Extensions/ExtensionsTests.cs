using System.Collections.Generic;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void ForEach_WithCollectionOf0_Iterates0Times()
        {
            var totalIterations = 0;
            var listOfNumbers = new List<int>();

            listOfNumbers.ForEach(number => totalIterations++);

            Assert.That(totalIterations, Is.EqualTo(0));
        }

        [Test]
        public void ForEach_WithCollectionOf1_Iterates1Time()
        {
            var totalIterations = 0;
            var listOfNumbers = new[] { 1 };

            listOfNumbers.ForEach(number => totalIterations++);

            Assert.That(totalIterations, Is.EqualTo(1));
        }

        [Test]
        public void ForEach_WithCollectionOf10_Iterates10Times()
        {
            var totalIterations = 0;
            var listOfNumbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            listOfNumbers.ForEach(number => totalIterations++);

            Assert.That(totalIterations, Is.EqualTo(10));
        }

        [Test]
        public void With_WithPlaceholder_ReplacesPlaceholderWithArguments()
        {
            Assert.That("Hello {0}".With("World"), Is.EqualTo("Hello World"));
        }

        [Test]
        public void With_WithNoPlaceholders_ReturnsOriginalString()
        {
            Assert.That("Hello World".With(), Is.EqualTo("Hello World"));
        }

        [Test]
        public void ToEnum_WithString_ShouldReturnHttpVerbEquivalent()
        {
            Assert.That("get".ToEnum<HttpVerb>(), Is.EqualTo(HttpVerb.GET));
            Assert.That("PoSt".ToEnum<HttpVerb>(), Is.EqualTo(HttpVerb.POST));
            Assert.That("PUT".ToEnum<HttpVerb>(), Is.EqualTo(HttpVerb.PUT));
            Assert.That("Delete".ToEnum<HttpVerb>(), Is.EqualTo(HttpVerb.DELETE));
        }

        [Test]
        public void Name_WithEnum_ShouldReturnStringEquivalent()
        {
            Assert.That(HttpVerb.GET.Name(), Is.EqualTo("GET"));
            Assert.That(HttpVerb.POST.Name(), Is.EqualTo("POST"));
            Assert.That(HttpVerb.PUT.Name(), Is.EqualTo("PUT"));
            Assert.That(HttpVerb.DELETE.Name(), Is.EqualTo("DELETE"));
        }

        [Test]
        public void CastInt_WithEnum_ShouldReturnEnumIntegerValue()
        {
            Assert.That(HttpVerb.GET.CastInt(), Is.EqualTo(0));
            Assert.That(HttpVerb.POST.CastInt(), Is.EqualTo(1));
            Assert.That(HttpVerb.PUT.CastInt(), Is.EqualTo(2));
            Assert.That(HttpVerb.DELETE.CastInt(), Is.EqualTo(3));
        }

        [Test]
        public void IsEmpty_WithNonEmptyString_ReturnsFalse()
        {
            Assert.That("Non-empty".IsEmpty(), Is.False);
        }

        [Test]
        public void IsEmpty_WithEmptyString_ReturnsTrue()
        {
            Assert.That("".IsEmpty(), Is.True);
        }

        [Test]
        public void PascalSplit_WithEmptyString_ReturnsEmptyCollection()
        {
            Assert.That("".PascalSplit(), Is.Empty);
        }

        [Test]
        public void PascalSplit_WithSingleWord_ReturnsCollectionWithSingleWord()
        {
            var values = "Word".PascalSplit();

            Assert.That(values.Length, Is.EqualTo(1));
            Assert.That(values[0], Is.EqualTo("word"));
        }

        [Test]
        public void PascalSplit_WithTwoWords_ReturnsCollectionWithBothWords()
        {
            var values = "Word1Word2".PascalSplit();

            Assert.That(values.Length, Is.EqualTo(2));
            Assert.That(values[0], Is.EqualTo("word1"));
            Assert.That(values[1], Is.EqualTo("word2"));
        }

        [Test]
        public void PascalSplit_WithFullSentence_ReturnsCollectionOfAllWords()
        {
            var values = "UsersRegisterAddNew".PascalSplit();

            Assert.That(values.Length, Is.EqualTo(4));
            Assert.That(values[0], Is.EqualTo("users"));
            Assert.That(values[1], Is.EqualTo("register"));
            Assert.That(values[2], Is.EqualTo("add"));
            Assert.That(values[3], Is.EqualTo("new"));
        }

        [Test]
        public void UrlEncode_WithAlpanumericString_ReturnsUnmodifiedString()
        {
            Assert.That("Hello123".UrlEncode(), Is.EqualTo("Hello123"));
        }

        [Test]
        public void UrlEncode_WithStringWithASpace_ReturnsEncodedString()
        {
            Assert.That("Hello World".UrlEncode(), Is.EqualTo("Hello+World"));
        }

        [Test]
        public void ParseAcceptHeader_WithCatchAll_FindsOneAcceptTypeWithPriority1()
        {
            var headers = "*/*".ParseAcceptHeader();
            Assert.That(headers["*/*"], Is.EqualTo(1));
        }

        [Test]
        public void ParseAcceptHeader_SingleTypeAndPriority_FindsCorrectTypeAndPriority()
        {
            var headers = "application/xml; q=0.9".ParseAcceptHeader();
            Assert.That(headers["application/xml"], Is.EqualTo(0.9));
        }

        [Test]
        public void ParseAcceptHeader_MultipleTypesAndPriorities_FindsCorrectTypesAndPriorities()
        {
            var headers = "application/xml,application/json,text/html;q=0.8,text/plain; q=0.7".ParseAcceptHeader();
            Assert.That(headers["application/xml"], Is.EqualTo(1));
            Assert.That(headers["application/json"], Is.EqualTo(1));
            Assert.That(headers["text/html"], Is.EqualTo(0.8));
            Assert.That(headers["text/plain"], Is.EqualTo(0.7));
        }
    }
}