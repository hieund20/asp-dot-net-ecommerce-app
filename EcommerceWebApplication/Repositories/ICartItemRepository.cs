using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddToCartAsync(Guid ProductId, Guid CartSessionId);
        //Task<Guid> GetCartId();
        Task<List<CartItem>> GetCartItemsAsync(Guid CartSessionId);
        Task<decimal> GetTotalAsync(Guid CartSessionId);
    }
}
