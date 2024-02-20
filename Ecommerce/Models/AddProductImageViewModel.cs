using System.ComponentModel.DataAnnotations;

namespace Ecommerce.UI.Models
{
    public class AddProductImageViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDesciption { get; set; }
        public Guid ProductId { get; set; }
    }
}
