using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Responses;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands
{
    public class UpdateCartCommand : IRequest<UpdateCartResponse>
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