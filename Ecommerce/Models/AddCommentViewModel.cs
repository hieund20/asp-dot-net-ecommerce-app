using System.ComponentModel.DataAnnotations;

namespace Ecommerce.UI.Models
{
    public class AddCommentViewModel
    {
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
