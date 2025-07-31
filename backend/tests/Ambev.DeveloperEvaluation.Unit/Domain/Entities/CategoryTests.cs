using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using FluentAssertions;
using System.Reflection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Category_Should_Create_With_Valid_Parameters()
        {
            var category = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.Random.Guid().ToString(), f.Commerce.Categories(1)[0]))
                .Generate();

            category.ExternalId.Should().NotBeNullOrEmpty();
            category.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Category_Should_Throw_Exception_With_Empty_ExternalId()
        {
            Action act = static () => _ = new Category("", "Valid Name");
            act.Should().Throw<ArgumentException>()
                .WithMessage("ExternalId cannot be empty. (Parameter 'externalId')");
        }

        [Fact]
        public void Category_Should_Throw_Exception_With_Empty_Name()
        {
            Action act = () => _ = new Category("ValidExternalId", "");
            act.Should().Throw<ArgumentException>()
                .WithMessage("Name cannot be empty. (Parameter 'name')");
        }

        [Fact(DisplayName = "Should create an instance using the private constructor")]
        public void Given_PrivateConstructor_When_Invoked_Then_ShouldCreateInstance()
        {
            var constructor = typeof(Category).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null, Type.EmptyTypes, null);

            constructor.Should().NotBeNull("The private constructor should exist.");

            var categoryInfo = (Category)constructor!.Invoke(null);

            categoryInfo.Should().NotBeNull();
            categoryInfo.ExternalId.Should().BeEmpty();
            categoryInfo.Name.Should().BeEmpty();
        }
    }
}
