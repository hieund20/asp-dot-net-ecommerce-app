using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class UpdateCommentRequestDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
