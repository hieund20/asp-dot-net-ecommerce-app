using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAllAsync(string? filterOn = null,
                                    string? filterQuery = null,
                                    string? sortBy = null,
                                    bool isAscending = true,
                                    int pageNumber = 1,
                                    int pageSize = 1000);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> UpdateAsync(Guid id, Product product);
        Task<Product?> DeleteAsync(Guid id);
        Task<int> GetTotalAsync();
    }
}
