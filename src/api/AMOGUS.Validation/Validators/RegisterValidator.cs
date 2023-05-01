using AMOGUS.Core.DataTransferObjects.User;
using FluentValidation;

namespace AMOGUS.Validation.Validators {
    internal class RegisterValidator : AbstractValidator<RegisterApiModel> {


        public RegisterValidator() {
            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("Email must be an valid email");

            RuleFor(e => e.UserName)
                .NotEmpty()
                .WithMessage("Username cannot be empty");

            RuleFor(e => e.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")
                .WithMessage("Password must have...\n\tat least one uppercase character\n\tat least one lowercase character\n\tat least one digit\n\tat least one special character\n\tat least 6 characters long");
        }
    }
}
