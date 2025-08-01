namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct.Results
{

    public class GetProductResult
    {
        public string Title { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string Image { get; private set; } = string.Empty;
        public string Category { get; private set; } = default!;
        public GetRatingResult Rating { get; private set; } = default!;
    }
}
