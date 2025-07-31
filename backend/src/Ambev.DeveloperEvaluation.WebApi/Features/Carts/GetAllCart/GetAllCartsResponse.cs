using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    public class GetAllCartsResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CreateCartItemResponse> Items { get; set; } = new();

    }
}