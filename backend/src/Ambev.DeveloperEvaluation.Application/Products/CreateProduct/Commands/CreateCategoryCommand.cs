namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands
{
    public class CreateCategoryCommand
    {
        public string ExternalId { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public CreateCategoryCommand(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }
    }
}
