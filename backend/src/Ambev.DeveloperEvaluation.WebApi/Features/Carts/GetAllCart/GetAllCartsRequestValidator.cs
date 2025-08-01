using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    public class GetAllCartsRequestValidator : AbstractValidator<GetAllCartsRequest>
    {
        public GetAllCartsRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than zero.");
        }
    }
}
