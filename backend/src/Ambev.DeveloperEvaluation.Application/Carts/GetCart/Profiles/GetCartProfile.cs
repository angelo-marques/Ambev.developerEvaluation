using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart.Profiles
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<Cart, GetCartResult>();

            CreateMap<CartItems, GetCartItemResult>();
        }
    }
}