using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsProfile : Profile
    {  
        public ListCartsProfile()
        {
            CreateMap<Cart, ListCartResult>()
                 .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CartItems, ListCartItemResult>();
        }
    }
}
