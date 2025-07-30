using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Responses;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Validators;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands
{
    /// <remarks>
    /// This command captures the necessary data to create a cart, 
    /// including the user ID, creation date, and a list of products.
    /// Implements <see cref="IRequest{TResponse}"/> to initiate a 
    /// request that returns a <see cref="CreateCartResponse"/>.
    /// The data provided in this command is validated using the 
    /// <see cref="CreateCartCommandValidator"/>, which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure the fields are 
    /// properly populated and comply with the required rules.
    /// </remarks>
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
