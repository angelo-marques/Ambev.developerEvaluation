using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Responses;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommandHandler : IRequestHandler<GetProductCommand, GetProductResponse>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public GetProductCommandHandler(
            IProductRepository ProductRepository,
            IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<GetProductResponse> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetByIdAsync(request.Id, cancellationToken);

            return product == null
                ? throw new KeyNotFoundException($"Product with ID {request.Id} not found")
                : _mapper.Map<GetProductResponse>(product);
        }
    }
}