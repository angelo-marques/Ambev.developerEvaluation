using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductPriceService _productPriceService;
        private readonly IMapper _mapper;
        public CreateCartHandler(ICartRepository cartRepository, IProductPriceService productPriceService, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productPriceService = productPriceService;
        }
  
        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var cart = new Cart(command.UserId);

            foreach (var product in command.Products)
            {
                var unitPrice = await _productPriceService.GetPriceAsync(product.ProductId);

                cart.UpdateProductQuantity(product.ProductId, product.Quantity, unitPrice);
            }

            var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);

            var response = _mapper.Map<CreateCartResult>(createdCart);

            return response;
        }
    }
}