using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Crosscutting.Common.Validation
{
    public class ValidationBehaviorTests
    {

        [Fact]
        public async Task Deve_Continuar_Se_Request_Valido()
        {

            // Arrange
            var validator = new TestValidator();
            var behavior = new ValidationBehavior<TestRequest, string>(new[] { validator });
            var request = new TestRequest { Campo = "valor válido" };

            // Act
            var resultado = await behavior.Handle(request, () => Task.FromResult("Sucesso"), CancellationToken.None);

            // Assert
            Assert.Equal("Sucesso", resultado);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Se_Request_Invalido()
        {
            // Arrange
            var validator = new TestValidator(retornaErro: true);
            var behavior = new ValidationBehavior<TestRequest, string>(new[] { validator });
            var request = new TestRequest { Campo = "" };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(request, () => Task.FromResult("Não deveria chegar aqui"), CancellationToken.None));
        }

        [Fact]
        public async Task Deve_Passar_Adiante_Se_Nao_Houver_Validators()
        {
            // Arrange
            var behavior = new ValidationBehavior<TestRequest, string>(Enumerable.Empty<IValidator<TestRequest>>());
            var request = new TestRequest();

            // Act
            var resultado = await behavior.Handle(request, () => Task.FromResult("Sem validação"), CancellationToken.None);

            // Assert
            Assert.Equal("Sem validação", resultado);
        }
    }

    public class TestRequest : IRequest<string>
    {
        public string Campo { get; set; } = string.Empty;
    }

    public class TestValidator : AbstractValidator<TestRequest>
    {
        public TestValidator(bool retornaErro = false)
        {
            if (retornaErro)
            {
                RuleFor(x => x.Campo)
                    .NotEmpty()
                    .WithMessage("Campo não pode ser vazio");
            }
        }
    }
}
