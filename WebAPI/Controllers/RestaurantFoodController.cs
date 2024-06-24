using Application.Interfaces;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantFoodController : ControllerBase
    {
        private readonly IRestaurantFoodLogic _restaurantFoodLogic;

        public RestaurantFoodController(IRestaurantFoodLogic restaurantFoodLogic)
        {
            _restaurantFoodLogic = restaurantFoodLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetRestaurantFoods()
        {
            try
            {
                var restaurantFoods = await _restaurantFoodLogic.GetAllRestaurantFoodsAsync();
                return Ok(restaurantFoods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{restaurantId}/{foodId}")]
        public async Task<IActionResult> GetRestaurantFood(Guid restaurantId, Guid foodId)
        {
            try
            {
                var restaurantFood = await _restaurantFoodLogic.GetRestaurantFoodByIdAsync(restaurantId, foodId);
                if (restaurantFood == null)
                {
                    return NotFound();
                }

                return Ok(restaurantFood);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<Restaurant_Food>> PostRestaurantFood(Restaurant_Food restaurantFood)
        {
            try
            {
                await _restaurantFoodLogic.AddRestaurantFoodAsync(restaurantFood);
                return CreatedAtAction("GetRestaurantFood", new { restaurantId = restaurantFood.Restaurant_ID, foodId = restaurantFood.Food_ID }, restaurantFood);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{restaurantId}/{foodId}")]
        public async Task<IActionResult> PutRestaurantFood(Guid restaurantId, Guid foodId, Restaurant_Food restaurantFood)
        {
            try
            {
                if (restaurantId != restaurantFood.Restaurant_ID || foodId != restaurantFood.Food_ID)
                {
                    return BadRequest();
                }

                await _restaurantFoodLogic.UpdateRestaurantFoodAsync(restaurantFood);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{restaurantId}/{foodId}")]
        public async Task<IActionResult> DeleteRestaurantFood(Guid restaurantId, Guid foodId)
        {
            try
            {
                var restaurantFood = await _restaurantFoodLogic.GetRestaurantFoodByIdAsync(restaurantId, foodId);
                if (restaurantFood == null)
                {
                    return NotFound();
                }

                await _restaurantFoodLogic.DeleteRestaurantFoodAsync(restaurantId, foodId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
