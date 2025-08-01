using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class DeleteCartHandlerTests
    {
        [Fact(DisplayName = "Dado carrinho existente quando excluir então retorna sucesso")]
        public async Task Handle_ExistingCart_ReturnsSuccess()
        {
            // Arrange
            var repository = Substitute.For<ICartRepository>();
            var cartId = Guid.NewGuid();
            var command = new DeleteCartCommand(cartId);
            repository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(true);
            var handler = new DeleteCartCommandHandler(repository);

            // Act
            DeleteCartResponse response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            await repository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado carrinho inexistente quando excluir então lança KeyNotFoundException")]
        public async Task Handle_NonexistentCart_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<ICartRepository>();
            var cartId = Guid.NewGuid();
            var command = new DeleteCartCommand(cartId);
            repository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(false);
            var handler = new DeleteCartCommandHandler(repository);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());
        }
    }
}