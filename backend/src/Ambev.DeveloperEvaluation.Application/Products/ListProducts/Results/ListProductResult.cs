
namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts.Results
{

    public class ListProductResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ListProductCategoryResult Category { get; set; } = default!;
        public ListProductRatingResponse Rating { get; set; } = default!;
    }
}