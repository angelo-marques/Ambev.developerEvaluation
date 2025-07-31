using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Results;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductResult, CreateProductResponse>();
            CreateMap<RatingRequest, CreateRatingCommand>().ReverseMap();
        }
    }
}

