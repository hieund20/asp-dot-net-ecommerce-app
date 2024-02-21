using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Ecommerce.UI.Models.DTO;
using System.Reflection;
using Ecommerce.UI.Models;

namespace Ecommerce.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public const string CartSessionKey = "CartId";
        public string ShoppingCartId { get; set; } = null;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            string shoppingCartId = GetCartId();

            List<CartItemDto> response = new List<CartItemDto>();
            try
            {
                //Get All Regions from Web API
                var httpResponseMessage = await client.GetAsync($"https://localhost:7168/api/CartItems/{shoppingCartId}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>());

            }
            catch (Exception ex)
            {

            }

            decimal total = 0;
            try
            {
                //Get All Regions from Web API
                total = await client.GetFromJsonAsync<decimal>($"https://localhost:7168/api/CartItems/GetCartItemsTotal/{shoppingCartId}");
            }
            catch (Exception ex)
            {

            }

            var viewData = new CartShowViewModel<List<CartItemDto>, decimal>
            {
                CartItemList = response,
                TotalPrice = total
            };

            return View(viewData);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(Guid ProductId)
        {
            var client = httpClientFactory.CreateClient();

            string shoppingCartId = GetCartId();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:7168/api/CartItems?ProductId={ProductId}&CartSessionId={shoppingCartId}"),
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<CartItemDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Cart");
            }

            return View("Index", "Cart");
        }

        public string GetCartId()
        {
            if (HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("CurrentUserId")))
                {
                    HttpContext.Session.SetString(CartSessionKey, HttpContext.Session.GetString("CurrentUserId").ToString());
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return HttpContext.Session.GetString(CartSessionKey).ToString();
        }

    }
}
