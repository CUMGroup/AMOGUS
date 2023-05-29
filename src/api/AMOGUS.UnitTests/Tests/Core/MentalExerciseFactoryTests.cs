using AMOGUS.Core.Factories;

namespace AMOGUS.UnitTests.Tests.Core {
    public class MentalExerciseFactoryTests {

        #region Median
        [Fact]
        public void Median_Returns0_ForEmptyArray() {
            var res = MentalExerciseFactory.Median(Array.Empty<int>());

            Assert.Equal(0, res);
        }

        [Fact]
        public void Median_ReturnsFirstElement_ForArrayWithSize1() {
            var res = MentalExerciseFactory.Median(new int[] { 2 });

            Assert.Equal(2, res);
        }

        [Fact]
        public void Median_ReturnsAverage_ForArrayWithSize2() {
            var res = MentalExerciseFactory.Median(new int[] { 2, 4 });

            Assert.Equal(3, res);
        }

        [Fact]
        public void Median_ReturnsMedian_ForSortedArrayWithEvenCount() {
            var res = MentalExerciseFactory.Median(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10});

            Assert.Equal(5.5, res);
        }

        [Fact]
        public void Median_ReturnsMedian_ForSortedArrayWithOddCount() {
            var res = MentalExerciseFactory.Median(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Assert.Equal(5.0, res);
        }

        [Fact]
        public void Median_ReturnsMedian_ForRandomArrayWithEvenCount() {
            var res = MentalExerciseFactory.Median(new int[] { 9, 10, 2, 7, 4, 8, 1, 3, 5, 6 });

            Assert.Equal(5.5, res);
        }

        [Fact]
        public void Median_ReturnsMedian_ForRandomArrayWithOddCount() {
            var res = MentalExerciseFactory.Median(new int[] { 9, 6, 7, 4, 2, 5, 8, 3, 1 });

            Assert.Equal(5.0, res);
        }

        #endregion

        #region CalcAnswer
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CalcAnswer_ReturnsEmpty_WithEmptyQuestion(string question) {

            var factory = new MentalExerciseFactory();
            var res = factory.CalcAnswer(question);

            Assert.Equal(string.Empty, res);
        }

        [Fact]
        public void CalcAnswer_SolvesEquation_WithXInQuestion() {

            var factory = new MentalExerciseFactory();
            var res = factory.CalcAnswer("x + 2 = 0");

            Assert.Equal("{ -2 }", res);
        }

        [Theory]
        [InlineData("2+10", "12")]
        [InlineData("2*10", "20")]
        [InlineData("2+10*2", "22")]
        [InlineData("2-10", "-8")]
        public void CalcAnswer_SolvesExpression_WithQuestion(string quest, string answer) {

            var factory = new MentalExerciseFactory();
            var res = factory.CalcAnswer(quest);

            Assert.Equal(answer, res);
        }

        [Fact]
        public void CalcAnswer_ReturnsEmpty_ForXInStatement() {

            var factory = new MentalExerciseFactory();
            var res = factory.CalcAnswer("x + 2");

            Assert.Equal(string.Empty, res);
        }

        #endregion

        #region CountElementsDivisibleBy

        [Theory]
        [InlineData(new int[] {}, 10, 0)]
        [InlineData(new int[] { 1, 2, 3 }, 10, 0)]
        [InlineData(new int[] { 10 }, 10, 1)]
        [InlineData(new int[] { 1, 2, 3, 5, 10 }, 10, 1)]
        [InlineData(new int[] { 10, 20, 30, 31 }, 10, 3)]
        [InlineData(new int[] { -10, 0, 30 }, 10, 3)]
        [InlineData(new int[] { 7, 14, 2 }, 7, 2)]
        [InlineData(new int[] { 0, 2, 3, 5, 10 }, 5, 3)]
        public void CountElementsDivisibleBy_Counts_Correctly(int[] input, int divBy, int expected) {

            var factory = new MentalExerciseFactory();

            var result = factory.CountElementsDivisibleBy(input, divBy);

            Assert.Equal(expected, result);
        }

        #endregion
    }
}
