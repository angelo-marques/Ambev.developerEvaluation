using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class CreateProductHandlerTests
    {
        [Fact(DisplayName = "Dado comando válido quando criar produto então retorna resultado com Id")]
        public async Task Handle_ValidCommand_ReturnsResult()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var mapper = Substitute.For<IMapper>();
            var categoryCommand = new CreateCategoryCommand("cat-1", "Bebidas");
            var ratingCommand = new CreateRatingCommand("rat-1", 4.5, 10);
            var command = new CreateProductCommand("Cerveja", 5m, "Descrição", "img.jpg", categoryCommand, ratingCommand);

            var product = new Product(
                command.Title,
                command.Price,
                command.Description,
                command.Image,
                new Category(command.Category.ExternalId, command.Category.Name),
                new Rating(command.Rating.ExternalId, command.Rating.AverageRate, command.Rating.TotalReviews)
            );

            var expectedResult = new CreateProductResult { Id = product.Id };

            mapper.Map<Product>(command).Returns(product);
            repository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);
            mapper.Map<CreateProductResult>(product).Returns(expectedResult);

            var handler = new CreateProductHandler(repository, mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            await repository.Received(1).CreateAsync(product, Arg.Any<CancellationToken>());
        }
    }
}