namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results
{
    public class ListCartResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceTotal { get; set; }
        public ICollection<ListCartItemResult> Products { get; set; } = [];
    }
}