using Ecommerce.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models.DTO
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
        public Product Product { get; set; }
    }
}
