using Ecommerce.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Ecommerce.UI.Models;

namespace Ecommerce.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> response = new List<ProductDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7168/api/products");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>());

            }
            catch (Exception ex)
            {

            }

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ProductDto>($"https://localhost:7168/api/products/{id.ToString()}");

            if (response is not null)
            {
                List<ProductImageDto> images = new List<ProductImageDto>();

                var httpResponseMessage = await client.GetAsync($"https://localhost:7168/api/ProductImages/{response.ProductId.ToString()}");

                httpResponseMessage.EnsureSuccessStatusCode();

                images.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductImageDto>>());
                
                response.ProductImages = images;

                return View(response);
            }

            return View(null);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7168/api/products"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Products");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ProductDto>($"https://localhost:7168/api/products/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7168/api/products/{request.ProductId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Products");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7168/api/products/{request.ProductId}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Products");

            }
            catch (Exception ex)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
