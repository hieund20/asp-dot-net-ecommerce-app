namespace Ecommerce.UI.Models
{
    public class CartShowViewModel<T1, T2>
    {
        public T1 CartItemList { get; set; }
        public T2 TotalPrice { get; set; }
    }
}
