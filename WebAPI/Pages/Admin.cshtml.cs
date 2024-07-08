using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Core.Entiteti;

namespace WebAPI.Pages
{
    public class AdminModel : PageModel
    {
        [BindProperty]
        public Restaurant Restaurant { get; set; }

        [BindProperty]
        public City City { get; set; }

        [BindProperty]
        public Food Food { get; set; }

        [BindProperty]
        public Food_category FoodCategory { get; set; }

        [BindProperty]
        public Restaurant_Food RestaurantFood { get; set; }

        public string RestaurantErrorMessage { get; set; }
        public string CityErrorMessage { get; set; }
        public string FoodErrorMessage { get; set; }
        public string FoodCategoryErrorMessage { get; set; }
        public string RestaurantFoodErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            Console.WriteLine("OnPostAsync called with action: " + action);

            switch (action)
            {
                case "addRestaurant":
                    return await AddRestaurantAsync();
                case "addCity":
                    return await AddCityAsync();
                case "addFood":
                    return await AddFoodAsync();
                case "addFoodCategory":
                    return await AddFoodCategoryAsync();
                case "addRestaurantFood":
                    return await AddRestaurantFoodAsync();
                case "editRestaurant":
                    return await EditRestaurantAsync();
                case "editCity":
                    return await EditCityAsync();
                case "editFood":
                    return await EditFoodAsync();
                case "editFoodCategory":
                    return await EditFoodCategoryAsync();
                case "editRestaurantFood":
                    return await EditRestaurantFoodAsync();
                case "deleteRestaurant":
                    return await DeleteRestaurantAsync();
                case "deleteCity":
                    return await DeleteCityAsync();
                case "deleteFood":
                    return await DeleteFoodAsync();
                case "deleteFoodCategory":
                    return await DeleteFoodCategoryAsync();
                case "deleteRestaurantFood":
                    return await DeleteRestaurantFoodAsync();
                default:
                    Console.WriteLine("Invalid action");
                    return Page();
            }
        }

        private async Task<IActionResult> AddRestaurantAsync()
        {
            Console.WriteLine("AddRestaurantAsync called");

            if (Restaurant == null || string.IsNullOrEmpty(Restaurant.Name) || Restaurant.CityId == null)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantErrorMessage = "Name and CityId are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(Restaurant);
            Console.WriteLine("Serialized Restaurant: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync("https://localhost:5001/api/Restaurant", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                RestaurantErrorMessage = "Failed to add restaurant.";
                return Page();
            }
        }

        private async Task<IActionResult> AddCityAsync()
        {
            Console.WriteLine("AddCityAsync called");

            if (City == null || string.IsNullOrEmpty(City.Name))
            {
                Console.WriteLine("ModelState is not valid");
                CityErrorMessage = "Name is required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(City);
            Console.WriteLine("Serialized City: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync("https://localhost:5001/api/City", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                CityErrorMessage = "Failed to add city.";
                return Page();
            }
        }

        private async Task<IActionResult> AddFoodAsync()
        {
            Console.WriteLine("AddFoodAsync called");

            if (Food == null || string.IsNullOrEmpty(Food.Name))
            {
                Console.WriteLine("ModelState is not valid");
                FoodErrorMessage = "Name is required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(Food);
            Console.WriteLine("Serialized Food: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync("https://localhost:5001/api/Food", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodErrorMessage = "Failed to add food.";
                return Page();
            }
        }

        private async Task<IActionResult> AddFoodCategoryAsync()
        {
            Console.WriteLine("AddFoodCategoryAsync called");

            if (FoodCategory == null || string.IsNullOrEmpty(FoodCategory.Category))
            {
                Console.WriteLine("ModelState is not valid");
                FoodCategoryErrorMessage = "Category is required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(FoodCategory);
            Console.WriteLine("Serialized Food Category: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync("https://localhost:5001/api/FoodCategory", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodCategoryErrorMessage = "Failed to add food category.";
                return Page();
            }
        }

        private async Task<IActionResult> AddRestaurantFoodAsync()
        {
            Console.WriteLine("AddRestaurantFoodAsync called");

            if (RestaurantFood == null || RestaurantFood.Restaurant_ID == Guid.Empty || RestaurantFood.Food_ID == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantFoodErrorMessage = "Restaurant ID and Food ID are required.";
                return Page();
            }

            // Kreiramo anonimni objekat sa samo potrebnim poljima
            var data = new
            {
                restaurant_ID = RestaurantFood.Restaurant_ID,
                food_ID = RestaurantFood.Food_ID
            };

            var json = JsonSerializer.Serialize(data);
            Console.WriteLine("Serialized RestaurantFood: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token is not available in cookies.");
                RestaurantFoodErrorMessage = "Authorization token is missing.";
                return Page();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await client.PostAsync("https://localhost:5001/api/RestaurantFood/link", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request succeeded");
                    return RedirectToPage("/Admin");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    Console.WriteLine("Response body: " + responseBody);
                    RestaurantFoodErrorMessage = "Failed to add restaurant food. " + responseBody;
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                RestaurantFoodErrorMessage = "An error occurred while adding restaurant food. " + ex.Message;
                return Page();
            }
        }

        private async Task<IActionResult> EditRestaurantAsync()
        {
            Console.WriteLine("EditRestaurantAsync called");

            if (Restaurant == null || Restaurant.Id == Guid.Empty || string.IsNullOrEmpty(Restaurant.Name) || Restaurant.CityId == null)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantErrorMessage = "Id, Name, and CityId are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(Restaurant);
            Console.WriteLine("Serialized Restaurant: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:5001/api/Restaurant/{Restaurant.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                RestaurantErrorMessage = "Failed to edit restaurant.";
                return Page();
            }
        }

        private async Task<IActionResult> EditCityAsync()
        {
            Console.WriteLine("EditCityAsync called");

            if (City == null || City.Id == Guid.Empty || string.IsNullOrEmpty(City.Name))
            {
                Console.WriteLine("ModelState is not valid");
                CityErrorMessage = "Id and Name are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(City);
            Console.WriteLine("Serialized City: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:5001/api/City/{City.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                CityErrorMessage = "Failed to edit city.";
                return Page();
            }
        }

        private async Task<IActionResult> EditFoodAsync()
        {
            Console.WriteLine("EditFoodAsync called");

            if (Food == null || Food.Id == Guid.Empty || string.IsNullOrEmpty(Food.Name))
            {
                Console.WriteLine("ModelState is not valid");
                FoodErrorMessage = "Id and Name are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(Food);
            Console.WriteLine("Serialized Food: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:5001/api/Food/{Food.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodErrorMessage = "Failed to edit food.";
                return Page();
            }
        }

        private async Task<IActionResult> EditFoodCategoryAsync()
        {
            Console.WriteLine("EditFoodCategoryAsync called");

            if (FoodCategory == null || FoodCategory.Id == Guid.Empty || string.IsNullOrEmpty(FoodCategory.Category))
            {
                Console.WriteLine("ModelState is not valid");
                FoodCategoryErrorMessage = "Id and Category are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(FoodCategory);
            Console.WriteLine("Serialized Food Category: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:5001/api/FoodCategory/{FoodCategory.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodCategoryErrorMessage = "Failed to edit food category.";
                return Page();
            }
        }

        private async Task<IActionResult> EditRestaurantFoodAsync()
        {
            Console.WriteLine("EditRestaurantFoodAsync called");

            if (RestaurantFood == null || RestaurantFood.Id == Guid.Empty || RestaurantFood.Restaurant_ID == Guid.Empty || RestaurantFood.Food_ID == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantFoodErrorMessage = "Id, Restaurant ID, and Food ID are required.";
                return Page();
            }

            var json = JsonSerializer.Serialize(RestaurantFood);
            Console.WriteLine("Serialized RestaurantFood: " + json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:5001/api/RestaurantFood/{RestaurantFood.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                RestaurantFoodErrorMessage = "Failed to edit restaurant food.";
                return Page();
            }
        }

        private async Task<IActionResult> DeleteRestaurantAsync()
        {
            Console.WriteLine("DeleteRestaurantAsync called");

            if (Restaurant == null || Restaurant.Id == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantErrorMessage = "Id is required.";
                return Page();
            }

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:5001/api/Restaurant/{Restaurant.Id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                RestaurantErrorMessage = "Failed to delete restaurant.";
                return Page();
            }
        }

        private async Task<IActionResult> DeleteCityAsync()
        {
            Console.WriteLine("DeleteCityAsync called");

            if (City == null || City.Id == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                CityErrorMessage = "Id is required.";
                return Page();
            }

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:5001/api/City/{City.Id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                CityErrorMessage = "Failed to delete city.";
                return Page();
            }
        }

        private async Task<IActionResult> DeleteFoodAsync()
        {
            Console.WriteLine("DeleteFoodAsync called");

            if (Food == null || Food.Id == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                FoodErrorMessage = "Id is required.";
                return Page();
            }

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:5001/api/Food/{Food.Id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodErrorMessage = "Failed to delete food.";
                return Page();
            }
        }

        private async Task<IActionResult> DeleteFoodCategoryAsync()
        {
            Console.WriteLine("DeleteFoodCategoryAsync called");

            if (FoodCategory == null || FoodCategory.Id == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                FoodCategoryErrorMessage = "Id is required.";
                return Page();
            }

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:5001/api/FoodCategory/{FoodCategory.Id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                FoodCategoryErrorMessage = "Failed to delete food category.";
                return Page();
            }
        }

        private async Task<IActionResult> DeleteRestaurantFoodAsync()
        {
            Console.WriteLine("DeleteRestaurantFoodAsync called");

            if (RestaurantFood == null || RestaurantFood.Id == Guid.Empty)
            {
                Console.WriteLine("ModelState is not valid");
                RestaurantFoodErrorMessage = "Id is required.";
                return Page();
            }

            using var client = new HttpClient();
            var token = HttpContext.Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:5001/api/RestaurantFood/{RestaurantFood.Id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request succeeded");
                return RedirectToPage("/Admin");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: " + responseBody);
                RestaurantFoodErrorMessage = "Failed to delete restaurant food.";
                return Page();
            }
        }
    }
}
