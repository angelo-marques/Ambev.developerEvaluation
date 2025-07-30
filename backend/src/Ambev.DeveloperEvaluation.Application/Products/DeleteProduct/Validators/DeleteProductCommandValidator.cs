using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }
}
