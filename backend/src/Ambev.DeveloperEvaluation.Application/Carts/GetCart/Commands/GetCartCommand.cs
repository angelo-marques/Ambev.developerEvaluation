using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands
{
    public record GetCartCommand : IRequest<GetCartResult>
    {
        public Guid Id { get; }

        public GetCartCommand(Guid id)
        {
            Id = id;
        }
    }
}