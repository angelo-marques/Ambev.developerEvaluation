using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        
        public CreateCartProfile()
        {
            CreateMap<CreateCartRequest, CreateCartCommand>();
            CreateMap<CreateCartResult, CreateCartResponse>();
            CreateMap<Cart, CreateCartResult>();
        }
    }
}
