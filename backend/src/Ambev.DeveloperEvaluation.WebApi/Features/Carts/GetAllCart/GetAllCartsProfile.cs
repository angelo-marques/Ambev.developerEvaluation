using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    public class GetAllCartsProfile : Profile
    {
        public GetAllCartsProfile()
        {
            CreateMap<GetAllCartsRequest, ListCartCommand>();
            CreateMap<ListCartResult, GetAllCartsResponse>();
        }
    }
}
