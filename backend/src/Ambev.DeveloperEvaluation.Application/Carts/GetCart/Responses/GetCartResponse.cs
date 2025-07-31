namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Responses
{
    public class GetCartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceTotal { get; set; }
        public List<GetCartItemResponse> Products { get; set; } = [];
    }
}
