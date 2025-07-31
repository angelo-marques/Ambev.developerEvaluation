using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Responses;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands
{
    public class CreateCartCommand : IRequest<CreateCartResponse>
    {
        public Guid UserId { get; private set; }

        public DateTime Date { get; private set; }

        public List<CreateCartItemCommand> Products { get; private set; } = [];

        public CreateCartCommand(Guid userId, DateTime date, List<CreateCartItemCommand> products)
        {
            UserId = userId;
            Date = date;
            Products = products;
        }
    }
}
