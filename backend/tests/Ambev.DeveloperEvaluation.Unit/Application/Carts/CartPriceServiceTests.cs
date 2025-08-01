using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services
{
    public class CartPriceServiceTests
    {
        [Fact(DisplayName = "Dado carrinho existente quando solicitar total então retorna valor total")]
        public async Task GetPriceTotalAsync_CartExists_ReturnsTotal()
        {
            // Arrange
            var repository = Substitute.For<ICartRepository>();
            var cartId = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.UpdateProductQuantity(Guid.NewGuid(), 3, 10m); // 30
            cart.UpdateProductQuantity(Guid.NewGuid(), 5, 10m); // 50 with discount 10% = 45
            cart.UpdateTotal();
            repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            var service = new CartPriceService(repository);

            // Act
            var total = await service.GetPriceTotalAsync(cartId);

            // Assert
            total.Should().Be(cart.PriceTotal);
            await repository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado carrinho inexistente quando solicitar total então lança KeyNotFoundException")]
        public async Task GetPriceTotalAsync_CartNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<ICartRepository>();
            var cartId = Guid.NewGuid();
            repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);
            var service = new CartPriceService(repository);

            // Act
            Func<Task> act = async () => await service.GetPriceTotalAsync(cartId);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
        }
    }
}