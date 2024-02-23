using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data
{
    public class EcommerceDBContext : DbContext
    {
        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
