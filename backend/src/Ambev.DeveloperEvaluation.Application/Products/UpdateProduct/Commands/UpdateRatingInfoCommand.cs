namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands
{
    public class UpdateRatingInfoCommand
    {       
        public string ExternalId { get; private set; } = string.Empty;

        public double AverageRate { get; private set; }

        public int TotalReviews { get; private set; }

        public UpdateRatingInfoCommand(string externalId, double averageRate, int totalReviews)
        {
            ExternalId = externalId;
            AverageRate = averageRate;
            TotalReviews = totalReviews;
        }
    }
}