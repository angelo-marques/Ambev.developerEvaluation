using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Responses;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);

            if (!success)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            return new DeleteProductResponse { Success = true };
        }
    }
}