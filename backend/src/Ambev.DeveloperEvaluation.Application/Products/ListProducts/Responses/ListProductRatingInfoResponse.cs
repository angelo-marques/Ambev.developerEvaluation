namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts.Responses;

public class ListProductRatingResponse
{
    public string ExternalId { get; set; } = string.Empty;
    public double AverageRate { get; set; }
    public int TotalReviews { get; set; }
}
