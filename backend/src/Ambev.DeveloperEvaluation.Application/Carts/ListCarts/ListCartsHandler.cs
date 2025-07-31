using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsHandler : IRequestHandler<PaginationQuery<ListCartResult>, PaginatedResult<ListCartResult>>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public ListCartsHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ListCartResult>> Handle(PaginationQuery<ListCartResult> request, CancellationToken cancellationToken)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.Order,
                cancellationToken: cancellationToken
            );

            var mappedCarts = _mapper.Map<ICollection<ListCartResult>>(paginatedResult.Items);

            return new PaginatedResult<ListCartResult>
            {
                Data = mappedCarts,
                CurrentPage = paginatedResult.CurrentPage,
                TotalPages = paginatedResult.TotalPages,
                TotalCount = paginatedResult.TotalItems
            };
        }
    }
}