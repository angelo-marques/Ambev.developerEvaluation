using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Responses;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands
{
    public record DeleteCartCommand : IRequest<DeleteCartResponse>
    {
        public Guid Id { get; }
        public DeleteCartCommand(Guid id)
        {
            Id = id;
        }
    }
}
