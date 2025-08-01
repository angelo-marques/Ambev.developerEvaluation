using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands
{
    public class CreateCartCommand : IRequest<CreateCartResult>
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
