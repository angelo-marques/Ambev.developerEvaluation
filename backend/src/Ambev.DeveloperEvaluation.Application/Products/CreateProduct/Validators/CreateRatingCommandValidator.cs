using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Validators
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.ExternalId)
                .NotEmpty().WithMessage("ExternalId is required.")
                .MaximumLength(255).WithMessage("ExternalId must not exceed 255 characters.");

            RuleFor(x => x.AverageRate)
                .InclusiveBetween(0.0, 5.0).WithMessage("AverageRate must be between 0.0 and 5.0.");

            RuleFor(x => x.TotalReviews)
                .GreaterThanOrEqualTo(0).WithMessage("TotalReviews cannot be negative.");
        }
    }
}
