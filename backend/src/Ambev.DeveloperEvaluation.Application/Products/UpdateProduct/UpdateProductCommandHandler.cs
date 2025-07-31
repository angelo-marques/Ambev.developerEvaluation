using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Responses;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command);

            var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);

            return _mapper.Map<UpdateProductResponse>(updatedProduct);
        }
    }
}
