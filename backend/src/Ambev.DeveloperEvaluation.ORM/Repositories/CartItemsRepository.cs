using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    internal class CartItemsRepository : ICartItemsRepository
    {
        private readonly DefaultContext _context;

        public CartItemsRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<CartItems> CreateAsync(CartItems cartItem, CancellationToken cancellationToken = default)
        {
            await _context.CartItems.AddAsync(cartItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cartItem;
        }

        /// <summary>
        /// Updates an existing cart item
        /// </summary>
        public async Task<CartItems> UpdateAsync(Guid id, CartItems cartItem, CancellationToken cancellationToken = default)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

            if (existingItem == null)
                throw new KeyNotFoundException($"Cart item with ID {id} not found.");

            await _context.SaveChangesAsync(cancellationToken);
            return existingItem;
        }

        public async Task<CartItems?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cartItem = await GetByIdAsync(id, cancellationToken);
            if (cartItem == null)
                return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves all cart items with pagination and ordering
        /// </summary>
        public async Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<CartItems>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
        {
            var query = _context.CartItems
                .Include(ci => ci.Cart)
                .Include(ci => ci.Product)
                .AsQueryable();

            // Ordenação dinâmica
            if (!string.IsNullOrWhiteSpace(order))
            {
                query = query.OrderBy(order);
            }

            // Paginação
            int totalItems = await query.CountAsync(cancellationToken);
            List<CartItems> cartItems = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new Ambev.DeveloperEvaluation.Common.Results.PagedResult<CartItems>
            {
                Data = cartItems,
                TotalItems = totalItems,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        public Task<CartItems> UpdateAsync(Guid id, CartItems cartItem, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<CartItems?> ICartItemsRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
