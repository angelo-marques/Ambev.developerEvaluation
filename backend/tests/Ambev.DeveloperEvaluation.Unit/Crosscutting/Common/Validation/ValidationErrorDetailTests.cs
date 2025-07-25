using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Crosscutting.Common.Validation
{
    public class ValidationErrorDetailTests
    {
        [Fact]
        public void Conversao_De_ValidationFailure_Deve_Retornar_Objeto_Corretamente()
        {
            // Arrange
            var failure = new ValidationFailure("Propriedade", "Mensagem de erro")
            {
                ErrorCode = "ERR001"
            };

            // Act
            var resultado = (ValidationErrorDetail)failure;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal("ERR001", resultado.Error);
                Assert.Equal("Mensagem de erro", resultado.Detail);
            });
        }

        [Fact]
        public void Objeto_Deve_Ter_Propriedades_Default_Quando_Instanciado_Sem_Failure()
        {
            // Arrange
            var detalhe = new ValidationErrorDetail();

            // Assert
            Assert.Multiple(() => {
                Assert.Equal(string.Empty, detalhe.Error);
                Assert.Equal(string.Empty, detalhe.Detail);
            });
        }
    }
}
