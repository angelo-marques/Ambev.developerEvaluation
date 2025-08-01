using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile()
        {
            CreateMap<Guid, DeleteProductCommand>()
                .ConstructUsing(id => new DeleteProductCommand(id));
        }
    }
}
