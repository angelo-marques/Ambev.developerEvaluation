using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsHandler : IRequestHandler<PaginationQuery<ListProductsQuery, ListProductResult>, PaginatedResult<ListProductResult>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ListProductsHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ListProductResult>> Handle(PaginationQuery<ListProductsQuery, ListProductResult> request, CancellationToken cancellationToken)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.Order,
                cancellationToken: cancellationToken
            );

            var mappedProducts = _mapper.Map<ICollection<ListProductResult>>(paginatedResult.Items);

            return new PaginatedResult<ListProductResult>
            {
                Data = mappedProducts,
                CurrentPage = paginatedResult.CurrentPage,
                TotalPages = paginatedResult.TotalPages,
                TotalCount = paginatedResult.TotalItems
            };
        }
    }
}
