using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartCommandHandler : IRequestHandler<GetCartCommand, GetCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GetCartResponse> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);

            return cart == null
                ? throw new KeyNotFoundException($"Cart with ID {request.Id} not found")
                : _mapper.Map<GetCartResponse>(cart);
        }
    }
}