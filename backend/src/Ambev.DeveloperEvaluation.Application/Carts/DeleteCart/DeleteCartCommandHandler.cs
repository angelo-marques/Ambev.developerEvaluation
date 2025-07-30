using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, DeleteCartResponse>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<DeleteCartResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var success = await _cartRepository.DeleteAsync(request.Id, cancellationToken);

            if (!success)
            {
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
            }

            return new DeleteCartResponse { Success = true };
        }
    }
}
