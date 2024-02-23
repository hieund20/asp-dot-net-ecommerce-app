using Ecommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Ecommerce.UI.Models.DTO;

namespace Ecommerce.UI.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public CommentsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            model.CreatedDate = DateTime.Now;

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7168/api/comments"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<CommentDto>();

            if (response is not null)
            {
                return RedirectToAction("Detail", "Products", new {ProductId = model.ProductId});
            }

            return View();
        }
    }
}
