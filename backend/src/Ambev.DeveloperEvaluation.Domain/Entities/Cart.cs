using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public decimal PriceTotal { get; private set; }
        public List<CartItems> Products { get; private set; } = [];

        private Cart() { }

        public Cart(Guid userId)
        {
            UserId = userId;
            Date = DateTime.UtcNow;
            Products = [];
        }

        public void UpdateProductQuantity(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity < 1 || quantity > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be between 1 and 20");
            }

            var existingItem = Products.SingleOrDefault(p => p.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.UpdateProductQuantity(quantity, unitPrice);
            }
            else
            {
                var newItem = new CartItems(productId, quantity, unitPrice);

                Products.Add(newItem);
            }
        }

        public void UpdateTotal()
        {
            PriceTotal = Products.Sum(p => p.PriceTotalWithDiscount);
        }
    }
}
