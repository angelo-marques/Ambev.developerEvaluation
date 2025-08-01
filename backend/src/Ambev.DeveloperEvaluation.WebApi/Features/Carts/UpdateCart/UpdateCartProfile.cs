using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartResult, UpdateCartResponse>();
            CreateMap<UpdateCartRequest, UpdateCartCommand>();
        }
    }
}
