namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartItemResponse
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}