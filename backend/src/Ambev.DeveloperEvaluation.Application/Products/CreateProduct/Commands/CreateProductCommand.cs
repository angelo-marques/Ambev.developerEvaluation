using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Responses;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public string Title { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string Image { get; private set; } = string.Empty;
        public CreateCategoryInfoCommand Category { get; private set; } = default!;
        public CreateRatingCommand Rating { get; private set; } = default!;
        public CreateProductCommand(string title, decimal price, string description, string image, CreateCategoryInfoCommand category, CreateRatingCommand rating)
        {
            Title = title;
            Price = price;
            Description = description;
            Image = image;
            Category = category;
            Rating = rating;
        }
        public ValidationResultDetail Validate()
        {
            var validator = new CreateProductCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}