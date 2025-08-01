using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Validators
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
   
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.Products)
                 .NotEmpty().WithMessage("Carts list cannot be empty.")
                 .Must(p => p != null && p.Count > 0).WithMessage("Carts list must contain at least one item.")
                 .ForEach(cart =>
                 {
                     cart.NotNull().WithMessage("Cart cannot be null.");
                     cart.SetValidator(new UpdateCartItemCommandValidator());
                 });
        }
    }
}
