
using AMOGUS.Core.Domain.Models.Entities;
using FluentValidation;

namespace AMOGUS.Validation.Validators {
    internal class GameSessionValidator : AbstractValidator<GameSession> {

        public GameSessionValidator() {

            RuleFor(x => x.SessionId)
                .NotEmpty()
                .WithMessage("SessionId cannot be empty")
                .Must(e => Guid.TryParse(e, out _))
                .WithMessage("SessionId must be a valid GUID");

            RuleFor(x => x.Playtime)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Playtime must be greater than or equal to 0");

            RuleFor(x => x.CorrectAnswersCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CorrectAnswerCount must be greater than or equal to 0");

            RuleFor(x => x.GivenAnswersCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("GivenAnswerCount must be greater than or equal to 0");

            RuleFor(x => x.AverageTimePerQuestion)
                .GreaterThanOrEqualTo(0)
                .WithMessage("AverageTimePerQuestion must be greater than or equal to 0");

            RuleFor(x => x.QuickestAnswer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("QuickestAnswer must be greater than or equal to 0");

            RuleFor(x => x.SlowestAnswer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("SlowestAnswer must be greater than or equal to 0");
        }
    }
}
