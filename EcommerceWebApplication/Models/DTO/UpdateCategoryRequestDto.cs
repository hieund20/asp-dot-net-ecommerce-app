using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
