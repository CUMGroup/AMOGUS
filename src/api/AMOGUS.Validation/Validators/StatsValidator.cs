
using AMOGUS.Core.Domain.Models.Entities;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Validation.Validators {
    internal class StatsValidator : AbstractValidator<UserStats> {

        public StatsValidator() {

            RuleFor(x => x.Level)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Level must be greater than or equal to 0");

            RuleFor(x => x.CurrentStreak)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Current Streak must be greater than or equal to 0");

            RuleFor(x => x.OverallAnswered)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Overall answered questions must be greater than or equal to 0");

            RuleFor(x => x.CorrectAnswers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Correct answered questions must be greater than or equal to 0");

            RuleFor(x => x.TotalTimePlayed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total time played must be greater than or equal to 0");

            RuleFor(x => x.QuickestAnswer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quickest Answer must be greater than or equal to 0");

            RuleFor(x => x.SlowestAnswer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Slowest Answer must be greater than or equal to 0");


            RuleFor(x => x.LongestStreak)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Longest Streak must be greater than or equal to 0")
                .GreaterThanOrEqualTo(x => x.CurrentStreak)
                .WithMessage("Longest Streak must be greater than or equal to current streak");
        }
    }
}
