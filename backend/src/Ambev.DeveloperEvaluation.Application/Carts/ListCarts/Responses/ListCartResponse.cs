namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Responses
{
    public class ListCartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceTotal { get; set; }
        public ICollection<ListCartItemResponse> Products { get; set; } = [];
    }
}