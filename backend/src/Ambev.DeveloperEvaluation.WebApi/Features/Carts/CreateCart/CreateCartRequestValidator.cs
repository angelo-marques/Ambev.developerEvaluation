using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
       
        public CreateCartRequestValidator()
        {
            RuleFor(cart => cart.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(cart => cart.Items)
                .NotEmpty().WithMessage("Cart must contain at least one item.");

            RuleForEach(cart => cart.Items)
                .SetValidator(new CreateCartItemRequestValidator());
        }
    }

  
    public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
    {
     
        public CreateCartItemRequestValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
