using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Ecommerce.UI.Models.DTO;
using System.Reflection;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginRequestDto loginRequest)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7168/api/Auth/Login"),
                Content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (response is not null)
            {
                var httpUserResponseMessage = await client.GetFromJsonAsync<IdentityUser>($"https://localhost:7168/api/User/GetUserFromToken?jwtToken={response.JwtToken}");

                if (httpUserResponseMessage is not null)
                {
                    HttpContext.Session.SetString("jwtToken", response.JwtToken.ToString());
                    HttpContext.Session.SetString("CurrentUserName", httpUserResponseMessage.UserName.ToString());
                    HttpContext.Session.SetString("CurrentUserEmail", httpUserResponseMessage.Email.ToString());
                    HttpContext.Session.SetString("CurrentUserId", httpUserResponseMessage.Id.ToString());


                    //Call API lấy role data từ user, lưu trong Session
                    var httpRolesResponseMessage = await client.GetFromJsonAsync<List<string>>($"https://localhost:7168/api/User/GetRolesFromToken?jwtToken={response.JwtToken}");
                    if (httpRolesResponseMessage is not null)
                    {
                        HttpContext.Session.SetString("CurrentUserRoles", JsonSerializer.Serialize(httpRolesResponseMessage));
                    }    
                }
                return RedirectToAction("Index", "Home");
            }

            return View("Login Faild");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7168/api/Auth/Register"),
                Content = new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            var response = httpResponseMessage.EnsureSuccessStatusCode(); //200 or 201

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View("Register Faild");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var client = httpClientFactory.CreateClient();

            var jwtToken = HttpContext.Session.GetString("jwtToken");

            var response = await client.GetFromJsonAsync<IdentityUser>($"https://localhost:7168/api/User/GetUserFromToken?jwtToken={jwtToken}");
            
            if (response is not null)
            {
                return View(response);
            }
            return View("Get user profile faild");
        }
    }
}
