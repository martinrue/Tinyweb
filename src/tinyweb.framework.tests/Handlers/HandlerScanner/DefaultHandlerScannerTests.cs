﻿using System;
using System.Linq;
using NUnit.Framework;
using tinyweb.framework.tests.TestArea;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultHandlerScannerTests
    {
        IHandlerScanner defaultHandlerScanner;
        ScanResult scanResult;

        [SetUp]
        public void Setup()
        {
            defaultHandlerScanner = new DefaultHandlerScanner();
            scanResult = AssemblyScanner.FindHandlersAndFilters(HandlerScanner.Current.GetSearcher(), FilterScanner.Current.GetSearcher());
        }

        [TearDown]
        public void Teardown()
        {
            Tinyweb.Areas.Clear();
        }

        [Test]
        public void FindAll_WhenCalled_FindsAllHandlers()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Count(), Is.EqualTo(15));
        }

        [Test]
        public void FindAll_WhenCalled_FirstHandlerTypeIsResource1Handler()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Any(h => h.Type == new Resource1Handler().GetType()));
        }

        [Test]
        public void FindAll_WhenCalled_FirstHandlerUriIsCorrect()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new Resource1Handler().GetType()).Uri == "resource1");
        }

        [Test]
        public void FindAll_WhenCalled_SecondHandlerTypeIsResource2Handler()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Any(h => h.Type == new Resource2Handler().GetType()));
        }

        [Test]
        public void FindAll_WhenCalled_SecondHandlerUriIsCorrect()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new Resource2Handler().GetType()).Uri == "resource2");
        }

        [Test]
        public void FindAll_WhenCalled_ThirdHandlerTypeIsResource3Handler()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Any(h => h.Type == new Resource3Handler().GetType()));
        }

        [Test]
        public void FindAll_WhenCalled_ThirdHandlerUriIsCorrect()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new Resource3Handler().GetType()).Uri == "resource3");
        }

        [Test]
        public void FindAll_WhenCalled_FourthHandlerTypeIsConventionHandler()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Any(h => h.Type == new ConventionHandler().GetType()));
        }

        [Test]
        public void FindAll_WhenCalled_FourthHandlerUriIsCorrect()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ConventionHandler().GetType()).Uri == "convention");
        }

        [Test]
        public void FindAll_WhenCalled_PascalConventionHandlerHasBeenLocated()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Any(h => h.Type == new PascalConventionHandler().GetType()));
        }

        [Test]
        public void FindAll_WhenCalled_PascalConventionHandlerHasCorrectUri()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new PascalConventionHandler().GetType()).Uri, Is.EqualTo("pascal/convention"));
        }

        [Test]
        public void FindAll_WhenCalled_RootHandlerHasEmptyUri()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new RootHandler().GetType()).Uri, Is.EqualTo(String.Empty));
        }

        [Test]
        public void FindAll_WhenCalled_ExplicitFieldRouteRootHandlerHasEmptyUri()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ExplicitFieldRouteRootHandler().GetType()).Uri, Is.EqualTo(String.Empty));
        }

        [Test]
        public void FindAll_WhenCalled_ExplicitPropertyRouteRootHandlerHasEmptyUri()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ExplicitPropertyRouteRootHandler().GetType()).Uri, Is.EqualTo(String.Empty));
        }

        [Test]
        public void FindAll_WhenCalled_ExplicitMethodRouteRootHandlerHasEmptyUri()
        {
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ExplicitMethodRouteRootHandler().GetType()).Uri, Is.EqualTo(String.Empty));
        }

        [Test]
        public void FindAll_WhenCalled_AreaResourceHandlerIncludesAreaInUri()
        {
            Tinyweb.RegisterArea("test", typeof(ResourceHandler).Namespace);
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ResourceHandler().GetType()).Uri, Is.EqualTo("test/resource"));
        }

        [Test]
        public void FindAll_WhenCalled_HandlerWithTheSameNameAsARegisteredAreaInANamespaceMapsToAreaRootUri()
        {
            Tinyweb.RegisterArea("test", typeof(TestHandler).Namespace);
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new TestHandler().GetType()).Uri, Is.EqualTo("test"));
        }

        [Test]
        public void FindAll_WhenCalled_AreaResourceHandlerWithExplicitRouteIncludesAreaInUri()
        {
            Tinyweb.RegisterArea("test", typeof(ExplicitRouteAreaHandler).Namespace);
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ExplicitRouteAreaHandler().GetType()).Uri, Is.EqualTo("test/foo/bar"));
        }

        [Test]
        public void FindAll_WhenCalled_AreaResourceHandlerWithExplicitRouteWithAreaDoesNotPrependAreaAgainInUri()
        {
            Tinyweb.RegisterArea("test", typeof(ExplicitRouteWithAreaHandler).Namespace);
            var handlers = defaultHandlerScanner.FindAll(scanResult.Handlers);
            Assert.That(handlers.Single(h => h.Type == new ExplicitRouteWithAreaHandler().GetType()).Uri, Is.EqualTo("test/foo/baz"));
        }
    }
}