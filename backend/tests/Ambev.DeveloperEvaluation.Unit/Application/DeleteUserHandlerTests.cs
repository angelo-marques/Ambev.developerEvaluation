using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class DeleteUserHandlerTests
    {
        [Fact(DisplayName = "Dado comando válido quando excluir usuário então retorna sucesso")]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);
            repository.DeleteAsync(userId, Arg.Any<CancellationToken>()).Returns(true);
            var handler = new DeleteUserHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            await repository.Received(1).DeleteAsync(userId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado usuário inexistente quando excluir usuário então lança KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);
            repository.DeleteAsync(userId, Arg.Any<CancellationToken>()).Returns(false);
            var handler = new DeleteUserHandler(repository);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).DeleteAsync(userId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado comando inválido quando excluir usuário então lança ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            var command = new DeleteUserCommand(Guid.Empty);
            var handler = new DeleteUserHandler(repository);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            await repository.DidNotReceive().DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }
    }
}