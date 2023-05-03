using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Validation.Validators;
using FluentValidation.TestHelper;

namespace AMOGUS.UnitTests.Tests.Validation {
    public class GameSessionValidatorTests {

        #region SessionId

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SessionId_CannotBeEmpty(string id) {
            var session = DefaultSession();
            session.SessionId = id;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.SessionId);
        }

        [Theory]
        [InlineData("asdlahsfndfasff")]
        [InlineData("84EECED2-30A6-40F7")]
        public void SessionId_ShouldBeValidGuid(string id) {
            var session = DefaultSession();
            session.SessionId = id;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.SessionId);
        }

        [Fact]
        public void SessionId_ShouldBeValid() {
            var session = DefaultSession();
            session.SessionId = "84EECED2-30A6-40F7-87E9-F3613B53FD6C";
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.SessionId);
        }

        #endregion

        #region Playtime

        [Fact]
        public void Playtime_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.Playtime = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.Playtime);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void Playtime_CanBeZeroOrBigger(double playtime) {
            var session = DefaultSession();
            session.Playtime = playtime;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.Playtime);
        }

        #endregion

        #region CorrectAnswersCount

        [Fact]
        public void CorrectAnswersCount_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.CorrectAnswersCount = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.CorrectAnswersCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CorrectAnswersCount_CanBeZeroOrBigger(int CorrectAnswersCount) {
            var session = DefaultSession();
            session.CorrectAnswersCount = CorrectAnswersCount;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.CorrectAnswersCount);
        }

        #endregion

        #region GivenAnswersCount

        [Fact]
        public void GivenAnswersCount_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.GivenAnswersCount = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.GivenAnswersCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GivenAnswersCount_CanBeZeroOrBigger(int GivenAnswersCount) {
            var session = DefaultSession();
            session.GivenAnswersCount = GivenAnswersCount;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.GivenAnswersCount);
        }

        #endregion

        #region AverageTimePerQuestion

        [Fact]
        public void AverageTimePerQuestion_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.AverageTimePerQuestion = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.AverageTimePerQuestion);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void AverageTimePerQuestion_CanBeZeroOrBigger(double AverageTimePerQuestion) {
            var session = DefaultSession();
            session.AverageTimePerQuestion = AverageTimePerQuestion;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.AverageTimePerQuestion);
        }

        #endregion

        #region QuickestAnswer

        [Fact]
        public void QuickestAnswer_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.QuickestAnswer = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.QuickestAnswer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void QuickestAnswer_CanBeZeroOrBigger(double QuickestAnswer) {
            var session = DefaultSession();
            session.QuickestAnswer = QuickestAnswer;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.QuickestAnswer);
        }

        #endregion

        #region SlowestAnswer

        [Fact]
        public void SlowestAnswer_ShouldNotBeSmallerThanZero() {
            var session = DefaultSession();
            session.SlowestAnswer = -1;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldHaveValidationErrorFor(x => x.SlowestAnswer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(1)]
        public void SlowestAnswer_CanBeZeroOrBigger(double SlowestAnswer) {
            var session = DefaultSession();
            session.SlowestAnswer = SlowestAnswer;
            var validator = new GameSessionValidator();

            var validResult = validator.TestValidate(session);

            validResult.ShouldNotHaveValidationErrorFor(x => x.SlowestAnswer);
        }

        #endregion

        private static GameSession DefaultSession() {
            return new GameSession {
                SessionId = "84EECED2-30A6-40F7-87E9-F3613B53FD6C",
                AverageTimePerQuestion = 1,
                CorrectAnswersCount = 1,
                GivenAnswersCount = 1,
                Playtime = 1,
                QuickestAnswer = 1,
                SlowestAnswer = 1,
            };
        }
    }
}
