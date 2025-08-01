using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductPriceService _productPriceService;
        private readonly IMapper _mapper;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productPriceService = Substitute.For<IProductPriceService>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartHandler(_cartRepository, _productPriceService, _mapper);
        }

        [Fact(DisplayName = "Dado um comando válido quando criar carrinho então retorna resultado com Id e chama dependências")]
        public async Task Handle_ValidCommand_ReturnsCreateCartResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<CreateCartItemCommand>
            {
                new CreateCartItemCommand(Guid.NewGuid(), 2),
                new CreateCartItemCommand(Guid.NewGuid(), 5)
            };
            var command = new CreateCartCommand(userId, DateTime.UtcNow, products);

            // Configura retorno do serviço de preço para cada produto
            foreach (var item in products)
            {
                _productPriceService.GetPriceAsync(item.ProductId).Returns(10m);
            }

            // Monta um carrinho de domínio com os valores calculados
            var cart = new Cart(userId);
            foreach (var item in products)
            {
                cart.UpdateProductQuantity(item.ProductId, item.Quantity, 10m);
            }
            cart.UpdateTotal();
            _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
                .Returns(cart);

            var expected = new CreateCartResult
            {
                Id = cart.Id
            };
            _mapper.Map<CreateCartResult>(cart).Returns(expected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(cart.Id);
            await _productPriceService.Received(products.Count).GetPriceAsync(Arg.Any<Guid>());
            await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado produto com quantidade inválida quando criar carrinho então lança exceção")]
        public async Task Handle_InvalidProductQuantity_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var products = new List<CreateCartItemCommand>
            {
                // Quantidade fora do intervalo permitido (1 a 20) para gerar exceção no domínio
                new CreateCartItemCommand(Guid.NewGuid(), 0)
            };
            var command = new CreateCartCommand(userId, DateTime.UtcNow, products);

            _productPriceService.GetPriceAsync(Arg.Any<Guid>()).Returns(10m);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
            await _cartRepository.DidNotReceive().CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }
    }
}