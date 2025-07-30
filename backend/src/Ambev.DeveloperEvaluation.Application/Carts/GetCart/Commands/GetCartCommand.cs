using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Responses;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands
{
    public record GetCartCommand : IRequest<GetCartResponse>
    {
        public Guid Id { get; }

        public GetCartCommand(Guid id)
        {
            Id = id;
        }
    }
}