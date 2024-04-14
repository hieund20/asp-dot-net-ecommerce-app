using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<List<Order>> GetAllAsync(string? filterOn = null,
                                    string? filterQuery = null,
                                    string? sortBy = null,
                                    bool isAscending = true,
                                    int pageNumber = 1,
                                    int pageSize = 1000);
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order?> UpdateAsync(Guid id, Order order);
        Task<Order?> DeleteAsync(Guid id);
    }
}
