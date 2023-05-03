using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Validation.Validators;
using FluentValidation.TestHelper;

namespace AMOGUS.UnitTests.Tests.Validation {
    public class RegisterValidatorTests {

        #region Email

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Email_CannotBeEmpty(string email) {
            var registerModel = DefaultRegisterModel();
            registerModel.Email = email;
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("ölajsdadw")]
        [InlineData("alex@")]
        [InlineData("@")]
        [InlineData("@hagl.de")]
        public void Email_CannotBeInvalid(string email) {
            var registerModel = DefaultRegisterModel();
            registerModel.Email = email;
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Email_IsValid() {
            var registerModel = DefaultRegisterModel();
            registerModel.Email = "test@test.de";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        #endregion

        #region Username

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Username_CannotBeEmpty(string username) {
            var registerModel = DefaultRegisterModel();
            registerModel.UserName = username;
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Username_IsValid() {
            var registerModel = DefaultRegisterModel();
            registerModel.UserName = "Test";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldNotHaveValidationErrorFor(x => x.UserName);
        }

        #endregion

        #region Password

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Password_CannotBeEmpty(string password) {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = password;
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Password_MustHaveOneLowercase() {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = "PASSWORD1!";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Password_MustHaveOneUppercase() {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = "password1!";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Password_MustHaveOneDigit() {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = "Passwordd!";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Password_MustHaveOneSpecialChar() {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = "Password11";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Password_MustBeAtLeastSixCharsLong() {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = "Lel1!";
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData("Password1!")]
        [InlineData("111Pa!")]
        [InlineData("11PPaa!!_")]
        [InlineData("a1a1aPaPaa!a!_a")]
        public void Password_ShouldBeValid(string password) {
            var registerModel = DefaultRegisterModel();
            registerModel.Password = password;
            var validator = new RegisterValidator();

            var validResult = validator.TestValidate(registerModel);

            validResult.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        #endregion

        private static RegisterApiModel DefaultRegisterModel() {
            return new RegisterApiModel {
                Email = "test@test.de",
                Password = "Password1!",
                UserName = "Test",
            };
        }
    }
}
