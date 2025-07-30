namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands
{
    public class UpdateCategoryInfoCommand
    {
        public string ExternalId { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public UpdateCategoryInfoCommand(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }
    }
}
