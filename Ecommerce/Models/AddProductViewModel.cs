namespace Ecommerce.UI.Models
{
    public class AddProductViewModel
    {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
