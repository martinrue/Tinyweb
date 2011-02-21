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
    }
}