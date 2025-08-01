using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        [Fact(DisplayName = "Dado produto existente quando excluir então retorna sucesso")]
        public async Task Handle_ExistingProduct_ReturnsSuccess()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);
            repository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(true);
            var handler = new DeleteProductHandler(repository);

            // Act
            DeleteProductResult result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            await repository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado produto inexistente quando excluir então lança KeyNotFoundException")]
        public async Task Handle_NonexistentProduct_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);
            repository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(false);
            var handler = new DeleteProductHandler(repository);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}