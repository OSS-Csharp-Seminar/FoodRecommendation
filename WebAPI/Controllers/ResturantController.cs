using Application.Interfaces;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RestourantController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;

        public RestourantController(IRestaurantLogic restaurantLogic)
        {
            _restaurantLogic = restaurantLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> RestaurantList()
        {
            var restaurants = await _restaurantLogic.GetRestaurantListAsync();
            return Ok(restaurants);
        }

        [HttpGet("byName")]
        public async Task<IActionResult> GetRestaurantsByName(string name)
        {
            var restaurants = await _restaurantLogic.GetRestaurantsByNameAsync(name);
            return Ok(restaurants);
        }

        [HttpGet("byCity")]
        public async Task<IActionResult> GetRestaurantsByCityName(string cityName)
        {
            var restaurants = await _restaurantLogic.GetRestaurantsByCityNameAsync(cityName);
            return Ok(restaurants);
        }
        // POST: api/Restaurant
        [HttpPost("")]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            await _restaurantLogic.AddRestaurantAsync(restaurant);
            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            await _restaurantLogic.UpdateRestaurantAsync(restaurant);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            var restaurant = await _restaurantLogic.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            await _restaurantLogic.DeleteRestaurantAsync(id);

            return NoContent();
        }
    }
}
