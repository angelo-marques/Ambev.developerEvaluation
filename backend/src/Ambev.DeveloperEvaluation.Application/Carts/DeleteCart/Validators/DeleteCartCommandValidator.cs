using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Validators
{
    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Cart ID is required");
        }
    }
}
