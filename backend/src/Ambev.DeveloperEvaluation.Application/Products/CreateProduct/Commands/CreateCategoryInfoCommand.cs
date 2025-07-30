namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands
{
    public class CreateCategoryInfoCommand
    {
        public string ExternalId { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public CreateCategoryInfoCommand(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }
    }
}
