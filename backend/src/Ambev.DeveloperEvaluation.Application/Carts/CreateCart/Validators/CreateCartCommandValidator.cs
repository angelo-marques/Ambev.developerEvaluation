using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Validators
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");

            RuleFor(x => x.Products)
                 .NotEmpty().WithMessage("Products list cannot be empty.")
                 .Must(p => p != null && p.Count > 0).WithMessage("Products list must contain at least one item.")
                 .ForEach(product =>
                 {
                     product.NotNull().WithMessage("Product cannot be null.");
                     product.SetValidator(new CreateCartItemCommandValidator());
                 });
        }
    }
}