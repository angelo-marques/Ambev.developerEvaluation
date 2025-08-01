using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public List<CreateCartItemResponse> Items { get; set; } = new();
    }
}
