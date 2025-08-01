using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CartTests
    {
        [Fact(DisplayName = "Dado quantidade entre 1 e 3 quando atualizar item então não aplica desconto")]
        public void UpdateItem_NoDiscountForLowQuantity()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());

            // Act
            cart.UpdateProductQuantity(productId, 2, 10m); // 2 unidades de 10 reais
            var item = cart.Products.Single();

            // Assert
            item.Quantity.Should().Be(2);
            item.UnitPrice.Should().Be(10m);
            item.PriceTotal.Should().Be(20m);
            item.Discount.Should().Be(0m);
            item.PriceTotalWithDiscount.Should().Be(20m);
        }

        [Fact(DisplayName = "Dado quantidade entre 4 e 9 quando atualizar item então aplica 10% de desconto")]
        public void UpdateItem_ApplyTenPercentDiscount()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            cart.UpdateProductQuantity(productId, 5, 10m); // 5 unidades
            var item = cart.Products.Single();

            // Assert
            item.Discount.Should().Be(0.10m);
            item.PriceTotalWithDiscount.Should().BeApproximately(45m, 0.0001m);
        }

        [Fact(DisplayName = "Dado quantidade entre 10 e 20 quando atualizar item então aplica 20% de desconto")]
        public void UpdateItem_ApplyTwentyPercentDiscount()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            cart.UpdateProductQuantity(productId, 12, 10m);
            var item = cart.Products.Single();

            // Assert
            item.Discount.Should().Be(0.20m);
            item.PriceTotalWithDiscount.Should().BeApproximately(96m, 0.0001m); // 12*10=120, -20% = 96
        }

        [Theory(DisplayName = "Dado quantidade inválida quando atualizar item então lança ArgumentOutOfRangeException")]
        [InlineData(0)]
        [InlineData(21)]
        public void UpdateItem_InvalidQuantity_ThrowsException(int quantity)
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            Action act = () => cart.UpdateProductQuantity(productId, quantity, 10m);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact(DisplayName = "Dado vários itens quando atualizar total então soma preços com desconto")]
        public void UpdateTotal_SumsDiscountedPrices()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid());
            cart.UpdateProductQuantity(Guid.NewGuid(), 2, 10m); // 20 sem desconto
            cart.UpdateProductQuantity(Guid.NewGuid(), 5, 10m); // 50 com 10% desconto = 45

            // Act
            cart.UpdateTotal();

            // Assert
            cart.PriceTotal.Should().BeApproximately(65m, 0.0001m);
        }
    }
}