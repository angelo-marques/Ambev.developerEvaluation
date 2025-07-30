namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands
{
    public class CreateRatingCommand
    {
        public string ExternalId { get; private set; } = string.Empty;

        public double AverageRate { get; private set; }

        public int TotalReviews { get; private set; }

        public CreateRatingCommand(string externalId, double averageRate, int totalReviews)
        {
            ExternalId = externalId;
            AverageRate = averageRate;
            TotalReviews = totalReviews;
        }
    }
}