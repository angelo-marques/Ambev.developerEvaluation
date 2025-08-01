using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartItemsRepository
    {
        Task<CartItems> CreateAsync(CartItems cartItem, CancellationToken cancellationToken = default);
        Task<CartItems> UpdateAsync(Guid id, CartItems cartItem, CancellationToken cancellationToken = default);
        Task<CartItems?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);      
    }
}
