using System;
using System.Collections.Generic;
using UnitTestingSamplesCore;
using Xunit;
using Xunit.Extensions;

namespace UnitTestingSamplesCoreTests
{
    public class StringSampleTests
    {
        [Fact]
        public void TestStringSampleNull()
        {
     
            Assert.Throws<ArgumentNullException>(() => new StringSample(null));
        }

        [Fact]
        public void TestGetStringDemoAB()
        {
            string expected = "b not found in a";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("a", "b");
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "a", "b", "b not found in a")]
        [InlineData("", "longer string", "nger", "removed nger from longer string: lo string")]
        [InlineData("init", "longer string", "string", "INIT")]
        public void TestGetStringDemo(string init, string a, string b, string expected)
        {
            var sample = new StringSample(init);
            string actual = sample.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetStringDemoExceptions()
        {
            var sample = new StringSample(string.Empty);
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo(null, "a"));
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo("a", null));
            Assert.Throws<ArgumentException>(() => sample.GetStringDemo(string.Empty, "a"));
        }

        [Theory]
        [MemberData(nameof(GetStringSampleData))]
        public void TestGetStringDemoUsingMember(string init, string a, string b, string expected)
        {
            var sample = new StringSample(init);
            string actual = sample.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> StringDemoData =>
            new[]
            {
                new object[] { "", "a", "b", "b not found in a" },
                new object[] { "", "longer string", "nger", "removed nger from longer string: lo string" },
                new object[] { "init", "longer string", "string", "INIT" }
            };

        public static IEnumerable<object[]> GetStringSampleData() =>
            new[]
            {
                new object[] { "", "a", "b", "b not found in a" },
                new object[] { "", "longer string", "nger", "removed nger from longer string: lo string" },
                new object[] { "init", "longer string", "string", "INIT" }
            };
    }
}
