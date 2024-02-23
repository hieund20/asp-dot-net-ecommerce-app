using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class AddProductRequestDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public Guid CategoryId { get; set; }
    }
}
