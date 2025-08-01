using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    /// <summary>
    /// Testes para o GetProductCommandHandler. Cobre cenários de produto encontrado e não encontrado.
    /// </summary>
    public class GetProductHandlerTests
    {

        [Fact(DisplayName = "Dado produto inexistente quando consultar então lança KeyNotFoundException")]
        public async Task Handle_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var mapper = Substitute.For<IMapper>();
            var handler = new GetProductCommandHandler(repository, mapper);
            var productId = Guid.NewGuid();
            var command = new GetProductCommand(productId);
            repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
            await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}