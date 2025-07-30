using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Responses;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands
{
    public class GetProductCommand : IRequest<GetProductResponse>
    {
        public Guid Id { get; set; }

        public GetProductCommand() { }
     
        public GetProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
