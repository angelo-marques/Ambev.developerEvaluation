using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Responses;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse>
    {
        public Guid Id { get; set; }

        public string Title { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public string Image { get; private set; } = string.Empty;

        public UpdateCategoryInfoCommand Category { get; private set; } = default!;
        
        public UpdateRatingInfoCommand Rating { get; private set; } = default!;

        public UpdateProductCommand(string title, decimal price, string description, string image, UpdateCategoryInfoCommand category, UpdateRatingInfoCommand rating)
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
            var validator = new UpdateProductCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}