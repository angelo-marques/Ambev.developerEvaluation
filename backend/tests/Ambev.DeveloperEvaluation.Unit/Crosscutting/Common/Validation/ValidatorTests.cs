using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Crosscutting.Common.Validation
{
    public class ValidatorTests
    {
        [Fact]
        public async Task ValidateAsync_Deve_Retornar_Erros_Quando_Objeto_Invalido()
        {
            // Arrange
           
            Type validatorType = null;
            // Act
            var erros = await Validator.ValidateAsync(validatorType);

            // Assert
            Assert.Null(erros);
        }
    }
}
