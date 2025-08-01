using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public record ListCartCommand(int Page, int Size, string Order) : IRequest<ListCartResult>;
}
