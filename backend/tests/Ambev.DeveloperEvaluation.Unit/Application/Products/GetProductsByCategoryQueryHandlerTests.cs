using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetProductsByCategoryQueryHandlerTests
    {
        [Fact(DisplayName = "Dado categoria válida quando consultar produtos então retorna resultado paginado")]
        public async Task Handle_ValidQuery_ReturnsPaginatedResult()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var handler = new GetProductsByCategoryQueryHandler(repository);
            var category = "Bebidas";
            var query = new GetProductsByCategoryQuery(category, page: 1, size: 10, order: "Title");

            var products = new List<Product>
            {
                new Product("Cerveja", 5m, "Descrição", "img.jpg", new Category("cat-1", category), new Rating("rat-1", 4.5, 10)),
                new Product("Refrigerante", 4m, "Descrição", "img.jpg", new Category("cat-1", category), new Rating("rat-1", 4.0, 8))
            };
            var expected = new PaginatedResult<Product>(products, totalItems: products.Count, currentPage: 1, size: 10);
            repository.GetProductsByCategoryAsync(category, 1, 10, "Title", Arg.Any<CancellationToken>()).Returns(expected);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalItems.Should().Be(2);
            await repository.Received(1).GetProductsByCategoryAsync(category, 1, 10, "Title", Arg.Any<CancellationToken>());
        }
    }
}