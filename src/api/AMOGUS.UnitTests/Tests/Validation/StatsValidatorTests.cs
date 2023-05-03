
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Validation.Validators;
using FluentValidation.TestHelper;

namespace AMOGUS.UnitTests.Tests.Validation {
    public class StatsValidatorTests {

        #region Level

        [Fact]
        public void Level_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.Level = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Level_CanBeZeroOrBigger(int Level) {
            var stats = DefaultStats();
            stats.Level = Level;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.Level);
        }

        #endregion

        #region CurrentStreak

        [Fact]
        public void CurrentStreak_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.CurrentStreak = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.CurrentStreak);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CurrentStreak_CanBeZeroOrBigger(int CurrentStreak) {
            var stats = DefaultStats();
            stats.CurrentStreak = CurrentStreak;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.CurrentStreak);
        }

        #endregion

        #region OverallAnswered

        [Fact]
        public void OverallAnswered_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.OverallAnswered = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.OverallAnswered);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void OverallAnswered_CanBeZeroOrBigger(int OverallAnswered) {
            var stats = DefaultStats();
            stats.OverallAnswered = OverallAnswered;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.OverallAnswered);
        }

        #endregion

        #region CorrectAnswers

        [Fact]
        public void CorrectAnswers_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.CorrectAnswers = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.CorrectAnswers);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CorrectAnswers_CanBeZeroOrBigger(int CorrectAnswers) {
            var stats = DefaultStats();
            stats.CorrectAnswers = CorrectAnswers;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.CorrectAnswers);
        }

        #endregion

        #region TotalTimePlayed

        [Fact]
        public void TotalTimePlayed_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.TotalTimePlayed = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.TotalTimePlayed);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void TotalTimePlayed_CanBeZeroOrBigger(double TotalTimePlayed) {
            var stats = DefaultStats();
            stats.TotalTimePlayed = TotalTimePlayed;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.TotalTimePlayed);
        }

        #endregion

        #region QuickestAnswer

        [Fact]
        public void QuickestAnswer_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.QuickestAnswer = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.QuickestAnswer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void QuickestAnswer_CanBeZeroOrBigger(double QuickestAnswer) {
            var stats = DefaultStats();
            stats.QuickestAnswer = QuickestAnswer;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.QuickestAnswer);
        }

        #endregion

        #region SlowestAnswer

        [Fact]
        public void SlowestAnswer_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.SlowestAnswer = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.SlowestAnswer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void SlowestAnswer_CanBeZeroOrBigger(double SlowestAnswer) {
            var stats = DefaultStats();
            stats.SlowestAnswer = SlowestAnswer;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.SlowestAnswer);
        }

        #endregion

        #region LongestStreak

        [Fact]
        public void LongestStreak_ShouldNotBeSmallerThanZero() {
            var stats = DefaultStats();
            stats.LongestStreak = -1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.LongestStreak);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void LongestStreak_CanBeZeroOrBigger(int LongestStreak) {
            var stats = DefaultStats();
            stats.LongestStreak = LongestStreak;
            stats.CurrentStreak = 0;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldNotHaveValidationErrorFor(x => x.LongestStreak);
        }

        [Fact]
        public void LongestStreak_CannotBeSmallerThanCurrentStreak() {
            var stats = DefaultStats();
            stats.LongestStreak = 0;
            stats.CurrentStreak = 1;
            var validator = new StatsValidator();

            var validResult = validator.TestValidate(stats);

            validResult.ShouldHaveValidationErrorFor(x => x.LongestStreak);
        }

        #endregion

        public static UserStats DefaultStats() {
            return new UserStats {
                Level = 1,
                CurrentStreak = 1,
                OverallAnswered = 1,
                CorrectAnswers = 1,
                TotalTimePlayed = 1,
                LongestStreak = 1,
                QuickestAnswer = 1,
                SlowestAnswer = 1,
            };
        }

    }
}
