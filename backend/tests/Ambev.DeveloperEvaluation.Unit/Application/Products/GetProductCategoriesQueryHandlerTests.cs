using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{  
    public class GetProductCategoriesQueryHandlerTests
    {
        [Fact(DisplayName = "Quando consultar categorias então retorna lista de categorias")]
        public async Task Handle_ReturnsCategories()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new GetProductCategoriesQueryHandler(repository);
            var query = new GetProductCategoriesQuery();
            var categories = new List<string> { "Bebidas", "Snacks" };
            repository.GetCategoriesAsync(Arg.Any<CancellationToken>()).Returns(categories);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(categories);
            await repository.Received(1).GetCategoriesAsync(Arg.Any<CancellationToken>());
        }
    }
}