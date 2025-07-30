using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class UserRegisteredEventTests
    {
        [Fact]
        public void Constructor_ShouldAssignUserCorrectly()
        {
            // Arrange
            // Given
            var command = CreateUserHandlerTestData.GenerateValidCommand();
            command.Password = "h@shedPassw0rd";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "João Silva",
                Password = command.Password,
                Email = command.Email,
                Phone = command.Phone,
                Status = command.Status,
                Role = command.Role
            };
           
            // Act
            var userEvent = new UserRegisteredEvent(user);

            // Assert
            Assert.NotNull(userEvent.User);
            Assert.Equal("João Silva", userEvent.User.Username);
        }
    }
}
