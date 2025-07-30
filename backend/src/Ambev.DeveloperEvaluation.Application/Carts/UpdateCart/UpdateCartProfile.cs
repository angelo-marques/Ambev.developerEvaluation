using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Responses;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<Cart, UpdateCartResponse>();
        }
    }
}
