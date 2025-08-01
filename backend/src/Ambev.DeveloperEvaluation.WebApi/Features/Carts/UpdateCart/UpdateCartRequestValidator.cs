using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {     
        public UpdateCartRequestValidator()
        {
            RuleFor(cart => cart.Id)
                .NotEmpty().WithMessage("Cart ID is required.");

            RuleFor(cart => cart.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(cart => cart.Items)
                .NotEmpty().WithMessage("Cart must contain at least one item.");

        }
    }
}
