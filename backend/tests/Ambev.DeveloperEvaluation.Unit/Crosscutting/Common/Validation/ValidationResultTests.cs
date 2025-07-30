using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Crosscutting.Common.Validation
{
    public class ValidationResultTests
    {
        [Fact]
        public void Construtor_Com_ValidationResult_Valido_Deve_Mapear_Corretamente()
        {
            // Arrange
            var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Campo1", "Mensagem 1") { ErrorCode = "E001" },
            new ValidationFailure("Campo2", "Mensagem 2") { ErrorCode = "E002" }
        };
            var validationResult = new ValidationResult(failures);

            // Act
            var resultado = new ValidationResultDetail(validationResult);
            var erroArray = resultado.Errors.ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.False(resultado.IsValid);
                Assert.NotNull(resultado.Errors);
                Assert.Equal(2, resultado.Errors.Count());

                Assert.Equal("E001", erroArray[0].Error);
                Assert.Equal("Mensagem 1", erroArray[0].Detail);
                Assert.Equal("E002", erroArray[1].Error);
                Assert.Equal("Mensagem 2", erroArray[1].Detail);
            });
        }

        [Fact]
        public void Construtor_Com_ValidationResult_Valido_Sem_Erros_Deve_Retornar_IsValid_True()
        {
            // Arrange
            var validationResult = new ValidationResult();

            // Act
            var resultado = new ValidationResultDetail(validationResult);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.True(resultado.IsValid);
                Assert.Empty(resultado.Errors);
            });
        }

        [Fact]
        public void Construtor_Default_Deve_Inicializar_Estado_Padrao()
        {
            // Arrange
            var resultado = new ValidationResultDetail();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.False(resultado.IsValid); // valor padrão do bool
                Assert.NotNull(resultado.Errors);
                Assert.Empty(resultado.Errors);
            });
        }
    }
}
