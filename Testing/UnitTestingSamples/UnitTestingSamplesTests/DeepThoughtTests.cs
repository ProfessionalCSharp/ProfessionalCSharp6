using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingSamples.Tests
{
    [TestClass]
    public class DeepThoughtTests
    {
        [TestMethod]
        public void TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverythingTest()
        {
            int expected = 42;
            var dt = new DeepThought();
            int actual =
              dt.TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything();
            Assert.AreEqual(expected, actual);
        }
    }
}