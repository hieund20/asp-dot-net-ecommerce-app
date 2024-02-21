using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class SQLCartItemRepository : ICartItemRepository
    {
        private readonly EcommerceDBContext dBContext;
        public const string CartSessionKey = "CartId";
        public string ShoppingCartId { get; set; }


        public SQLCartItemRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<CartItem> AddToCartAsync(Guid ProductId, Guid CartSessionId)
        {
            // Retrieve the product from the database.
            var cartItem = await dBContext.CartItems.SingleOrDefaultAsync(
                 c => c.CartId == CartSessionId && c.ProductId == ProductId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid(),
                    ProductId = ProductId,
                    CartId = CartSessionId,
                    Product = await dBContext.Products.SingleOrDefaultAsync(p => p.ProductId == ProductId),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                dBContext.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }

            await dBContext.SaveChangesAsync();

            return cartItem;
        }

        //public Task<Guid> GetCartId()
        //{
        //    if (HttpContext.Current.Session[CartSessionKey] == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
        //        {
        //            HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
        //        }
        //        else
        //        {
        //            // Generate a new random GUID using System.Guid class.     
        //            Guid tempCartId = Guid.NewGuid();
        //            HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
        //        }
        //    }
        //    return HttpContext.Current.Session[CartSessionKey].ToString();
        //}

        public async Task<List<CartItem>> GetCartItemsAsync(Guid CartSessionId)
        {
            return await dBContext.CartItems.Where(c => c.CartId == CartSessionId).Include(c => c.Product).ToListAsync();
        }

        public async Task<decimal> GetTotalAsync(Guid CartSessionId)
        {
            decimal total = decimal.Zero;
            decimal? sum = await (from cartItems in dBContext.CartItems
                                  where cartItems.CartId == CartSessionId
                                  select (decimal?)(cartItems.Quantity * cartItems.Product.UnitPrice)).SumAsync();

            total = sum ?? decimal.Zero;
            return total;
        }

        public async Task<int> GetCountAsync(Guid CartSessionId)
        {
            // Get the count of each item in the cart and sum them up          
            int? count = await ((from cartItems in dBContext.CartItems
                          where cartItems.CartId == CartSessionId
                                 select (int?)cartItems.Quantity)).SumAsync();
            // Return 0 if all entries are null         
            return count ?? 0;
        }
    }
}
