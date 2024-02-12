using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class UpdateProductRequestDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
