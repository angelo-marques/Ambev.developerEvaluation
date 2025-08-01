namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Results
{
    public class GetCartResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceTotal { get; set; }
        public List<GetCartItemResult> Products { get; set; } = [];
    }
}
