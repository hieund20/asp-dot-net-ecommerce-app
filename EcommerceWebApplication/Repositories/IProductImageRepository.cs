using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories
{
    public interface IProductImageRepository
    {
        Task<ProductImage> Upload(ProductImage image);
        Task<List<ProductImage>> GetAllByProductIdAsync(Guid id);
        Task<ProductImage> GetByIdAsync(Guid id);
    }
}
