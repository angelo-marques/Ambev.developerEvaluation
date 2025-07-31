namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct.Results
{
    public class GetRatingResult
    {
        public string ExternalId { get; private set; } = string.Empty;
        public double AverageRate { get; private set; }
        public int TotalReviews { get; private set; }
    }
}
