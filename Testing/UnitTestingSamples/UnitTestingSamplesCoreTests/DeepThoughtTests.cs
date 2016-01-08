using UnitTestingSamplesCore;
using Xunit;

namespace UnitTestingSamplesCoreTests
{
    public class DeepThoughtTests
    {
        [Fact]
        public void TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverythingTest()
        {
            int expected = 42;
            var dt = new DeepThought();
            int actual =
              dt.TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything();
            Assert.Equal(expected, actual);
        }
    }
}
