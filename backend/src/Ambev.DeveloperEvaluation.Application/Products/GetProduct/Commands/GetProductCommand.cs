using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands
{
    public class GetProductCommand : IRequest<GetProductResult>
    {
        public Guid Id { get; set; }

        public GetProductCommand() { }
     
        public GetProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
