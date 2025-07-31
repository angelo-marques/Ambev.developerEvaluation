using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct.Validators
{
    public class GetProductCommandValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}