using Ecommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Ecommerce.UI.Models.DTO;
using System.Reflection;

namespace Ecommerce.UI.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductImagesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid ProductId)
        {

            List<ProductImageDto> response = new List<ProductImageDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync($"https://localhost:7168/api/ProductImages/{ProductId.ToString()}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductImageDto>>());

            }
            catch (Exception ex)
            {

            }

            ViewBag.ProductId = ProductId;
            return View(response);
        }

        [HttpGet]
        public IActionResult Add(Guid ProductId)
        {
            ViewBag.ProductId = ProductId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductImageViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var formData = new MultipartFormDataContent();

            // Add other form fields
            formData.Add(new StringContent(model.FileName), "FileName");
            formData.Add(new StringContent(model.FileDesciption is null ? "" : model.FileDesciption), "FileDesciption");
            formData.Add(new StringContent(model.ProductId.ToString()), "ProductId");

            formData.Add(new StreamContent(model.File.OpenReadStream()), "File", model.File.FileName);

            var httpResponseMessage = await client.PostAsync("https://localhost:7168/api/ProductImages/Upload", formData);

            httpResponseMessage.EnsureSuccessStatusCode(); // 200 or 201

            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(response))
            {
                return RedirectToAction("Index", "ProductImages", new { ProductId = model.ProductId });
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ProductImageDto>($"https://localhost:7168/api/ProductImages/GetById/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductImageDto request)
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
                return RedirectToAction("Edit", "ProductImages");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductImageDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7168/api/ProductImages/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "ProductImages", new { ProductId = request.ProductId });

            }
            catch (Exception ex)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
