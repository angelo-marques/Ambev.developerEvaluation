using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class ListCartsHandlerTests
    {
        private readonly ICartRepository _cartRepository = Substitute.For<ICartRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly ListCartsHandler _handler;

        public ListCartsHandlerTests()
        {
            _handler = new ListCartsHandler(_cartRepository, _mapper);
        }

        [Fact]
        public async Task Handle_Should_ReturnPaginatedCarts()
        {
            var paginationQuery = new PaginationQuery<ListCartResult>(
                pageNumber: 1,
                pageSize: 10,
                order: "Date"
            );

            var paginatedResult = GenerateFakePaginatedCarts();
            var mappedCarts = paginatedResult.Items.Select(c => GenerateFakeListCartResponse(c)).ToList();

            _cartRepository.GetPaginatedAsync(
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            ).Returns(paginatedResult);

            _mapper.Map<ICollection<ListCartResult>>(paginatedResult.Items).Returns(mappedCarts);

            var result = await _handler.Handle(paginationQuery, CancellationToken.None);

            result.Should().NotBeNull();
            result.Data.Should().HaveCount(paginatedResult.Items.Count);
            result.CurrentPage.Should().Be(paginatedResult.CurrentPage);
            result.TotalPages.Should().Be(paginatedResult.TotalPages);
            result.TotalCount.Should().Be(paginatedResult.TotalItems);
        }

        private static DeveloperEvaluation.Domain.Pagination.PaginatedResult<Cart> GenerateFakePaginatedCarts()
        {
            var carts = new List<Cart>();

            for (int i = 0; i < 10; i++)
            {
                var cart = new Cart(userId: Guid.NewGuid());
                cart.UpdateProductQuantity(productId: Guid.NewGuid(), quantity: 1, 1);
                carts.Add(cart);
            }

            return new DeveloperEvaluation.Domain.Pagination.PaginatedResult<Cart>
            {
                Items = carts,
                CurrentPage = 1,
                TotalPages = 1,
                TotalItems = carts.Count
            };
        }

        private static ListCartResult GenerateFakeListCartResponse(Cart cart)
        {
            return new ListCartResult
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.ConvertAll(p => new ListCartItemResult
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                })
            };
        }
    }
}
