using System;
using System.Collections.Generic;
using NUnit.Framework;
using tinyweb.framework.Helpers;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class UrlTests
    {
        [SetUp]
        public void Setup()
        {
            Tinyweb.Handlers = new List<HandlerData>();
        }

        [Test]
        public void For_WithValidHandler_ReturnsHandlerUri()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "users/register", Type = typeof(Resource1Handler) }
            };

            Assert.That(Url.For<Resource1Handler>(), Is.EqualTo("/users/register"));
        }

        [Test]
        public void For_WithInalidHandler_ThrowsException()
        {
            Assert.Throws<Exception>(() => { Url.For<Resource1Handler>(); });
        }

        [Test]
        public void For_WithParameterisedHandler_ReplacesParametersWithArguments()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "the/{p1}/brown/{p2}", Type = typeof(Resource1Handler) }
            };

            var url = Url.For<Resource1Handler>(new { p1 = "quick", p2 = "fox jumped" });

            Assert.That(url, Is.EqualTo("/the/quick/brown/fox+jumped"));
        }

        [Test]
        public void For_WithRepeatedParameterisedHandler_ReplacesParametersWithArguments()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "some/{arg1}/{arg1}/{arg2}/{arg2}", Type = typeof(Resource1Handler) }
            };

            var url = Url.For<Resource1Handler>(new { arg1 = "fancy", arg2 = "url" });

            Assert.That(url, Is.EqualTo("/some/fancy/fancy/url/url"));
        }

        [Test]
        public void For_WithArgumentsButNoParameters_AddsArgumentsToQueryString()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "someurl", Type = typeof(Resource1Handler) }
            };

            var url = Url.For<Resource1Handler>(new { step1 = "the", step2 = "quick", step3 = "brown" });

            Assert.That(url, Is.EqualTo("/someurl?step1=the&step2=quick&step3=brown"));
        }

        [Test]
        public void For_WithArgumentsAndParameters_ReplacesParametersAndAddsArgumentsToQueryString()
        {
            Tinyweb.Handlers = new List<HandlerData>
            {
                new HandlerData { Uri = "someurl/{step1}", Type = typeof(Resource1Handler) }
            };

            var url = Url.For<Resource1Handler>(new { step1 = "the", step2 = "quick", step3 = "brown" });

            Assert.That(url, Is.EqualTo("/someurl/the?step2=quick&step3=brown"));
        }
    }
}