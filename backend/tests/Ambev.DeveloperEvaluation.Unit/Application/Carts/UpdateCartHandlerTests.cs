using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
   
    public class UpdateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductPriceService _productPriceService;
        private readonly IMapper _mapper;
        private readonly UpdateCartHandler _handler;

        public UpdateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productPriceService = Substitute.For<IProductPriceService>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateCartHandler(_cartRepository, _productPriceService, _mapper);
        }

        [Fact(DisplayName = "Dado carrinho existente quando atualizar então retorna resultado mapeado")]
        public async Task Handle_ExistingCart_UpdatesAndReturnsResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var products = new List<UpdateCartItemCommand>
            {
                new UpdateCartItemCommand(Guid.NewGuid(), 2),
                new UpdateCartItemCommand(Guid.NewGuid(), 4)
            };
            var command = new UpdateCartCommand(userId, products) { Id = cartId };

            // Configura carrinho existente no repositório
            var cart = new Cart(userId);
            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);

            // Preços de cada produto
            foreach (var item in products)
            {
                _productPriceService.GetPriceAsync(item.ProductId).Returns(10m);
            }

            // Configura retorno de atualização do repositório
            var updatedCart = new Cart(userId);
            _cartRepository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(updatedCart);

            var expected = new UpdateCartResult { Id = updatedCart.Id };
            _mapper.Map<UpdateCartResult>(updatedCart).Returns(expected);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expected.Id);
            await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
            await _productPriceService.Received(products.Count).GetPriceAsync(Arg.Any<Guid>());
            await _cartRepository.Received(1).UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado carrinho inexistente quando atualizar então lança KeyNotFoundException")]
        public async Task Handle_CartNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new UpdateCartCommand(Guid.NewGuid(), new List<UpdateCartItemCommand>()) { Id = Guid.NewGuid() };
            _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Cart?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _cartRepository.DidNotReceive().UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Dado quantidade inválida quando atualizar então lança ArgumentOutOfRangeException")]
        public async Task Handle_InvalidQuantity_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var products = new List<UpdateCartItemCommand>
            {
                new UpdateCartItemCommand(Guid.NewGuid(), 25) // quantidade inválida (>20)
            };
            var command = new UpdateCartCommand(userId, products) { Id = cartId };

            var cart = new Cart(userId);
            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            _productPriceService.GetPriceAsync(Arg.Any<Guid>()).Returns(10m);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
            await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
            await _cartRepository.DidNotReceive().UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }
    }
}