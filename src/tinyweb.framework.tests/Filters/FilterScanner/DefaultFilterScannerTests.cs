using System;
using System.Linq;
using NUnit.Framework;

namespace tinyweb.framework.tests
{
    [TestFixture]
    public class DefaultFilterScannerTests
    {
        IFilterScanner defaultFilterScanner;

        [SetUp]
        public void Setup()
        {
            defaultFilterScanner = new DefaultFilterScanner();
        }

        [Test]
        public void FindAll_WhenCalled_FindsAllFilters()
        {
            var filters = defaultFilterScanner.FindAll();
            Assert.That(filters.Count(), Is.EqualTo(7));
        }

        [Test]
        public void FindAll_WhenCalled_CorrectlyMarksAfterFilter()
        {
            var filter = defaultFilterScanner.FindAll().Single(f => f.Type == typeof(AfterFilter));
            Assert.That(filter.BeforeFilter, Is.False);
            Assert.That(filter.AfterFilter, Is.True);
        }

        [Test]
        public void FindAll_WhenCalled_CorrectlyMarksBeforeFilter()
        {
            var filter = defaultFilterScanner.FindAll().Single(f => f.Type == typeof(BeforeFilter));
            Assert.That(filter.BeforeFilter, Is.True);
            Assert.That(filter.AfterFilter, Is.False);
        }

        [Test]
        public void FindAll_WhenCalled_CorrectlyMarksBeforeAndAfterFilter()
        {
            var filter = defaultFilterScanner.FindAll().Single(f => f.Type == typeof(BeforeAndAfterFilter));
            Assert.That(filter.BeforeFilter, Is.True);
            Assert.That(filter.AfterFilter, Is.True);
        }

        [Test]
        public void FindAll_WhenCalled_IncludesExpectedFilters()
        {
            var filters = defaultFilterScanner.FindAll();
            Assert.That(filters.Any(f => f.Type == typeof(BeforeFilter)));
            Assert.That(filters.Any(f => f.Type == typeof(BeforeAndAfterFilter)));
            Assert.That(filters.Any(f => f.Type == typeof(AfterFilter)));
            Assert.That(filters.Any(f => f.Type == typeof(NonPriorityFilter)));
        }

        [Test]
        public void FindAll_WhenCalled_FindsCorrectFilterPriorities()
        {
            var filters = defaultFilterScanner.FindAll();
            Assert.That(filters.Single(f => f.Type == typeof(BeforeFilter)).Priority, Is.EqualTo(3));
            Assert.That(filters.Single(f => f.Type == typeof(BeforeAndAfterFilter)).Priority, Is.EqualTo(2));
            Assert.That(filters.Single(f => f.Type == typeof(AfterFilter)).Priority, Is.EqualTo(1));
            Assert.That(filters.Single(f => f.Type == typeof(NonPriorityFilter)).Priority, Is.EqualTo(Int32.MaxValue));
        }
    }
}