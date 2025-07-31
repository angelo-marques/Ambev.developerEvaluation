namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Category
    {
        public string ExternalId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;

        private Category() { }

        public Category(string externalId, string name)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentException("ExternalId cannot be empty.", nameof(externalId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            ExternalId = externalId;
            Name = name;
        }
    }
}
