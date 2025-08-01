using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services
{
    
    public class ProductPriceServiceTests
    {
        [Fact(DisplayName = "Dado produto existente quando solicitar preço então retorna preço")]
        public async Task GetPriceAsync_ProductExists_ReturnsPrice()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var productId = Guid.NewGuid();
            var product = new Product("Cerveja", 5m, "Descrição", "img.jpg", new Category("cat-1", "Bebidas"), new Rating("rat-1", 4.5, 10));
            repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
            var service = new ProductPriceService(repository);

            // Act
            var price = await service.GetPriceAsync(productId);

            // Assert
            price.Should().Be(product.Price);
            await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado produto inexistente quando solicitar preço então lança KeyNotFoundException")]
        public async Task GetPriceAsync_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var productId = Guid.NewGuid();
            repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);
            var service = new ProductPriceService(repository);

            // Act
            Func<Task> act = async () => await service.GetPriceAsync(productId);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}