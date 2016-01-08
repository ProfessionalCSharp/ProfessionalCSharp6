using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestingSamples.Tests
{
    [TestClass]
    public class StringSampleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStringSampleNull()
        {
            var sample = new StringSample(null);
        }

        [TestMethod]
        public void GetStringDemoAB()
        {
            string expected = "b not found in a";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("a", "b");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringDemoABCDBC()
        {
            string expected = "removed bc from abcd: ad";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("abcd", "bc");
            Assert.AreEqual(expected, actual);
        }
    }
}