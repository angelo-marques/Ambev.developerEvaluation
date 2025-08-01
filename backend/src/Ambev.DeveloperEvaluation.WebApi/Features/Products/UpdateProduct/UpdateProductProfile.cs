using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Results;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {   
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();
        }
    }
}
