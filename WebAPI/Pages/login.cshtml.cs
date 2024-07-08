using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = await GetTokenAsync(Password);
            if (!string.IsNullOrEmpty(token))
            {
                // Set the token in cookies
                HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });

                return RedirectToPage("/Admin");
            }
            else
            {
                ErrorMessage = "Invalid password.";
                return Page();
            }
        }

        public async Task<string> GetTokenAsync(string password)
        {
            using var client = new HttpClient();
            var loginModel = new { Password = password };
            var loginJson = JsonSerializer.Serialize(loginModel);
            var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");
            string contentString = await loginContent.ReadAsStringAsync();
            Console.WriteLine("Serialized Login Content: " + contentString);
            var loginResponse = await client.PostAsync("https://localhost:5001/api/Auth/login", loginContent);

            if (loginResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("loginResponse.IsSuccessStatusCode");
                var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
                var loginData = JsonDocument.Parse(loginResponseBody);
                var token = loginData.RootElement.GetProperty("token").GetString();
                Console.WriteLine("Token: " + token);
                return token;
            }
            else
            {
                Console.WriteLine("Login failed with status code: " + loginResponse.StatusCode);
                return null;
            }
        }
    }
}
