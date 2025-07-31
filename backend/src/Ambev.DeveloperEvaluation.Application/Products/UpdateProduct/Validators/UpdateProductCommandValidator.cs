using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {     
        public UpdateProductCommandValidator()
        {
            RuleFor(product => product.Title)
                .NotEmpty()
                .WithMessage("The product title must not be empty.");

            RuleFor(product => product.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The product price must be greater than or equal to 0.");

            RuleFor(product => product.Description)
                .NotEmpty()
                .WithMessage("The product description must not be empty.");

            RuleFor(product => product.Image)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .When(product => !string.IsNullOrWhiteSpace(product.Image))
                .WithMessage("The product image must be a valid URL.");

            RuleFor(product => product.Category)
                .NotNull()
                .WithMessage("The product category must not be null.")
                .SetValidator(new UpdateCategoryCommandValidator());

            RuleFor(product => product.Rating)
                .NotNull()
                .WithMessage("The product rating must not be null.")
                .SetValidator(new UpdateRatingInfoCommandValidator());
        }
    }
}