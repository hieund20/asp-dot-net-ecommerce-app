using Ecommerce.Models;
using Ecommerce.UI.Models;
using Ecommerce.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();

            //Get Prouct List
            List<ProductDto> response = new List<ProductDto>();
            try
            {
                var httpResponseMessage = await client.GetAsync("https://localhost:7168/api/products");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>());

            }
            catch (Exception ex)
            {
                
            }

            //Get ProductImage
            List<ProductDto> finalResponse = new List<ProductDto>();
            try
            {
                foreach (var product in response)
                {
                    List<ProductImageDto> productImageResponse = new List<ProductImageDto>();

                    var httpResponseMessage = await client.GetAsync($"https://localhost:7168/api/ProductImages/{product.ProductId}");

                    httpResponseMessage.EnsureSuccessStatusCode();

                    productImageResponse.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductImageDto>>());
                   
                    product.ThumbnailImageUrl = productImageResponse[0].FilePath;

                    finalResponse.Add(product);
                }
            }
            catch (Exception ex)
            { 
                
            }

            //Get Product Total Count
            PaginationDto pagination = new PaginationDto();
            try
            {
                var totalResponse = await client.GetFromJsonAsync<int>($"https://localhost:7168/api/Products/Total");
                pagination.TotalPage = (int)Math.Ceiling((decimal)totalResponse / 10); 
            }
            catch (Exception ex)
            {
                
            }

            var viewData = new HomeShowProductViewModel<List<ProductDto>, PaginationDto>
            {
                ProductList = finalResponse,
                Pagination = pagination
            };

            return View(viewData);
        }

        [HttpPost]
        public async Task<IActionResult> SearchProductByName()
        {
            string ProductName = Request.Form["ProductName"]; // Access form field directly

            List<ProductDto> response = new List<ProductDto>();
            try
            {
                //Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync($"https://localhost:7168/api/products?filterOn=ProductName&filterQuery={ProductName}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>());

                //Get ProductImage
                List<ProductDto> finalResponse = new List<ProductDto>();
                try
                {
                    foreach (var product in response)
                    {
                        List<ProductImageDto> productImageResponse = new List<ProductImageDto>();

                        var httpResponseProductImageMessage = await client.GetAsync($"https://localhost:7168/api/ProductImages/{product.ProductId}");

                        httpResponseProductImageMessage.EnsureSuccessStatusCode();

                        productImageResponse.AddRange(await httpResponseProductImageMessage.Content.ReadFromJsonAsync<IEnumerable<ProductImageDto>>());

                        product.ThumbnailImageUrl = productImageResponse[0].FilePath;

                        finalResponse.Add(product);
                    }
                }
                catch (Exception ex)
                {

                }
                return View("Index", finalResponse);
            }  
            catch (Exception ex)
            { 
                return View("Index", ex.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}