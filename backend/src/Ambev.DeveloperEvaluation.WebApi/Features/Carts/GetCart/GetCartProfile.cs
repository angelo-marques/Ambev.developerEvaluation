using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Results;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<Guid, GetCartCommand>()
                .ConstructUsing(id => new GetCartCommand(id));
            CreateMap<GetCartResult, GetCartResponse>();
        }
    }
}
