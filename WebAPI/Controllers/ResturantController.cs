using Application.Interfaces;
using Core.Entiteti;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantLogic _restaurantLogic;

        public RestaurantController(IRestaurantLogic restaurantLogic)
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

        [HttpPost("")]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            if (restaurant == null || restaurant.CityId == Guid.Empty)
            {
                return BadRequest("CityId is required.");
            }

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

        [HttpGet("{id}")]

        public async Task<IActionResult> GetRestaurantById(Guid id)
        {
            var restaurant = await _restaurantLogic.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
    }
}
