namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts.Results
{
    public class ListProductRatingResponse
    {
        public string ExternalId { get; set; } = string.Empty;
        public double AverageRate { get; set; }
        public int TotalReviews { get; set; }
    }
}