using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; private set; }

        public List<UpdateCartItemCommand> Products { get; private set; } = [];

        public UpdateCartCommand(Guid userId, List<UpdateCartItemCommand> products)
        {
            UserId = userId;
            Products = products;
        }
    }
}