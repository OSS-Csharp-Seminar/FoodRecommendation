﻿using Application.Interfaces;
using Core.Entiteti;
using Core.Common;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Core.Exceptions;

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
            var result = restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                CityId = r.CityId,
                City = r.City?.Name,
                Restaurant_Foods = r.Restaurant_Foods.Select(rf => new RestaurantFoodDto
                {
                    Food_ID = rf.Food_ID,
                    Restaurant_ID = rf.Restaurant_ID
                }).ToList()
            }).ToList();

            return Ok(result);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRestaurant([FromBody] Restaurant restaurant)
        {
            if (restaurant == null || restaurant.CityId == null)
            {
                return BadRequest("Invalid data.");
            }
            try
            {
                var city = await _cityLogic.GetCityByIdAsync(restaurant.CityId.Value);
                restaurant.City = city;

                await _restaurantLogic.AddRestaurantAsync(restaurant);
                return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
            try
            {
                var restaurant = await _restaurantLogic.GetRestaurantByIdAsync(id);
                if (restaurant == null)
                {
                    return NotFound();
                }
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
