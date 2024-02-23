using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.API.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDBContext dBContext;

        public SQLCategoryRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dBContext.Categories.AddAsync(category);
            await dBContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);

            if (existingCategory == null)
            {
                return null;
            }

            dBContext.Categories.Remove(existingCategory);
            await dBContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await dBContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryID==id);
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;

            await dBContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
