using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands
{
    public class DeleteProductCommand : IRequest<DeleteProductResult>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
