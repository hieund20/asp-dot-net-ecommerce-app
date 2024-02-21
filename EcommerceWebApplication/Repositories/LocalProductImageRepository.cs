using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class LocalProductImageRepository : IProductImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly EcommerceDBContext dBContext;

        public LocalProductImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, EcommerceDBContext dBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dBContext = dBContext;
        }

        public async Task<ProductImage?> DeleteAsync(Guid id)
        {
            var existingProductImage = await dBContext.ProductImages.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProductImage == null)
            {
                return null;
            }

            dBContext.ProductImages.Remove(existingProductImage);
            await dBContext.SaveChangesAsync();
            return existingProductImage;
        }

        public async Task<List<ProductImage>> GetAllByProductIdAsync(Guid id)
        {
            var images = await dBContext.ProductImages.ToListAsync();

            List<ProductImage> result = new List<ProductImage>();

            foreach (var image in images)
            {
                if (image.ProductId == id)
                {
                    result.Add(image);
                }    
            }

            return result;
        }

        public async Task<ProductImage> GetByIdAsync(Guid id)
        {
            return await dBContext.ProductImages.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductImage> Upload(ProductImage image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //Add Image to the Image table
            await dBContext.ProductImages.AddAsync(image);
            await dBContext.SaveChangesAsync();

            return image;
        }
    }
}
