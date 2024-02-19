namespace Ecommerce.UI.Models
{
    public class HomeShowProductViewModel<T1, T2>
    {
        public T1 ProductList { get; set; }
        public T2 Pagination { get; set; }
    }
}
