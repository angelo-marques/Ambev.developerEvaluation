using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser.Validators
{
    public class GetCartCommandValidator : AbstractValidator<GetUserCommand>
    {
        public GetCartCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required");
        }
    }
}
