using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Validators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.ExternalId)
                .NotEmpty().WithMessage("ExternalId is required.")
                .MaximumLength(255).WithMessage("ExternalId must not exceed 255 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("ExternalId must not exceed 255 characters.");
        }
    }
}