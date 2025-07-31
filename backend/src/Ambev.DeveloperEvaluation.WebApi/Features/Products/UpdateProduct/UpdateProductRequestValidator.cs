using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {      
        public UpdateProductRequestValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(product => product.Title)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(product => product.Image)
                  .NotEmpty().WithMessage("Description is required.");
        }
    }
}
