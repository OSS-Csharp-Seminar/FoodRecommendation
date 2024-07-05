using Application.Interfaces;
using Core.Entiteti;
using Core.Common;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly ICityLogic _cityLogic;

        public RestaurantController(IRestaurantLogic restaurantLogic, ICityLogic cityLogic)
        {
            _restaurantLogic = restaurantLogic;
            _cityLogic = cityLogic;
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

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            if (restaurantDto == null || restaurantDto.CityId == null)
            {
                return BadRequest("Invalid data.");
            }

            var city = await _cityLogic.GetCityByIdAsync(restaurantDto.CityId);
            if (city == null)
            {
                return NotFound("City not found.");
            }

            if (await _restaurantLogic.RestaurantExistsAsync(restaurantDto.Name))
            {
                return Conflict("Restaurant with the same name already exists.");
            }

            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                CityId = restaurantDto.CityId,
            };

            await _restaurantLogic.AddRestaurantAsync(restaurant);

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
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
