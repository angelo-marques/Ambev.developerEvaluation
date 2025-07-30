using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateUserValidatorTests
    {
        private readonly CreateUserCommandValidator _validator = new();

        [Fact]
        public void Should_HaveError_When_NameIsEmpty()
        {
            var model = new CreateUserCommand { Username = "", Email = "email@test.com", Password = "123456", Role = UserRole.Admin};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void Should_HaveError_When_EmailIsInvalid()
        {
            var model = new CreateUserCommand { Username = "User", Email = "invalidemail", Password = "123456", Role = UserRole.Admin };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_HaveError_When_PasswordTooShort()
        {
            var model = new CreateUserCommand { Username = "", Email = "email@test.com", Password = "123", Role = UserRole.Admin };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_NotHaveErrors_When_ModelIsValid()
        {
            var model = new CreateUserCommand { Username = "User", Email = "devloper@gmail.com", Password = "at34@as56", Role = UserRole.Admin, Status = UserStatus.Active, Phone = "+1 2799999254"  };
            var result = _validator.TestValidate(model);
            result.IsValid.Equals(true);
        }
    }
}
