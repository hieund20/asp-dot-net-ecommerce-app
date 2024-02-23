using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly EcommerceDBContext dBContext;

        public SQLProductRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dBContext.Products.AddAsync(product);
            await dBContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var existingProduct = await dBContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (existingProduct != null)
            {
                return null;
            }

            dBContext.Products.Remove(existingProduct);
            await dBContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null,
                                                string? filterQuery = null,
                                                string? sortBy = null,
                                                bool isAscending = true,
                                                int pageNumber = 1,
                                                int pageSize = 1000)
        {
            var products = dBContext.Products.AsQueryable();
            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => x.ProductName.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(x => x.ProductName) : products.OrderByDescending(x => x.ProductName);
                }
                else if (sortBy.Equals("UnitPrice", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(x => x.UnitPrice) : products.OrderByDescending(x => x.UnitPrice);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await products.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dBContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<int> GetTotalAsync()
        {
            return await dBContext.Products.CountAsync();
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await dBContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Description = product.Description;

            await dBContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
