using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<Cart, UpdateCartResult>();
        }
    }
}
