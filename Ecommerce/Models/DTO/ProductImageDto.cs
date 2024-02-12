using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.UI.Models.DTO
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDesciption { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public Guid ProductId { get; set; }

        //Navigation propertiesz    
        public ProductDto Product { get; set; }
    }
}
