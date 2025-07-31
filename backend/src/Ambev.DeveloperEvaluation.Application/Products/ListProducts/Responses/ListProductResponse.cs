
namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts.Responses;

public class ListProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ListProductCategoryResponse Category { get; set; } = default!;
    public ListProductRatingResponse Rating { get; set; } = default!;
}
