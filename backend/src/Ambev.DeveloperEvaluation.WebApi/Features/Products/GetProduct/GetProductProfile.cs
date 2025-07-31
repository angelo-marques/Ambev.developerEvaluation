using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            CreateMap<Guid, GetProductCommand>()
                .ConstructUsing(id => new GetProductCommand(id));
            CreateMap<Product, GetProductResult>();
            CreateMap<GetProductResult, GetProductResponse>();
        }
    }
}
