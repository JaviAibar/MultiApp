using MultiApp.Core.Extensions;
using System.Globalization;

namespace ExtensionsTest
{
    public class ExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        [SetCulture("en-UK")]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("1.1", 1.1d)]
        public void ParseOr0Test(string received, double expected)
        {
            System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.CurrentUICulture);
            Assert.That(received.ParseOr0(), Is.EqualTo(expected));
        }
    }
}