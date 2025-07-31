using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Profiles
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<Cart, GetCartResponse>();

            CreateMap<CartItems, GetCartItemResponse>();
        }
    }
}