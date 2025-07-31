using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<Cart, CreateCartResponse>();
        }
    }
}
