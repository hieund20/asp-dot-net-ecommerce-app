using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class AddCommentRequestDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
