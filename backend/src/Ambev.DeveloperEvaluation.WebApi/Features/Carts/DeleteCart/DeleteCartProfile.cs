using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart
{
    public class DeleteCartProfile : Profile
    {
        public DeleteCartProfile()
        {
            CreateMap<Guid, DeleteCartCommand>()
                .ConstructUsing(id => new DeleteCartCommand(id));
        }
    }
}
