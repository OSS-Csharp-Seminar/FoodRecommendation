using Application.Interfaces;
using Application.Services;
using Core.Dtos;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RestaurantFoodController : ControllerBase
{
    private readonly IRestaurantFoodLogic _restaurantFoodLogic;
    private readonly IRestaurantLogic _restaurantLogic;
    private readonly IFoodLogic _foodLogic;

    public RestaurantFoodController(IRestaurantFoodLogic restaurantFoodLogic, IRestaurantLogic restaurantLogic, IFoodLogic foodLogic)
    {
        _restaurantFoodLogic = restaurantFoodLogic;
        _restaurantLogic = restaurantLogic;
        _foodLogic = foodLogic;
    }

    [HttpPost("link")]
    public async Task<IActionResult> PostRestaurantFood([FromBody] RestaurantFoodDto restaurantFoodDto)
    {
        if (restaurantFoodDto == null || !restaurantFoodDto.Food_ID.HasValue || !restaurantFoodDto.Restaurant_ID.HasValue)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            var restaurant = await _restaurantLogic.GetRestaurantByIdAsync(restaurantFoodDto.Restaurant_ID.Value);
            var food = await _foodLogic.GetFoodByIdAsync(restaurantFoodDto.Food_ID.Value);

            if (restaurant == null || food == null)
            {
                return NotFound("Restaurant or Food not found.");
            }

            var existingLink = await _restaurantFoodLogic.GetRestaurantFoodAsync(restaurantFoodDto.Restaurant_ID, restaurantFoodDto.Food_ID);

            if (existingLink == null)
            {
                var restaurantFood = new Restaurant_Food
                {
                    Restaurant_ID = restaurantFoodDto.Restaurant_ID.Value,
                    Food_ID = restaurantFoodDto.Food_ID.Value,
                    Restaurant = restaurant,
                    Food = food
                };

                await _restaurantFoodLogic.AddRestaurantFoodAsync(restaurantFood);

                var result = new RestaurantFoodDto
                {
                    Food_ID = restaurantFood.Food_ID,
                    Restaurant_ID = restaurantFood.Restaurant_ID
                };

                return CreatedAtAction(nameof(GetRestaurantFood), new { restaurantId = restaurantFood.Restaurant_ID, foodId = restaurantFood.Food_ID }, result);
            }
            else
            {
                return BadRequest("The link between the restaurant and food already exists.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} - {ex.InnerException?.Message}");
        }
    }

    [HttpGet("{restaurantId}/{foodId}")]
    public async Task<IActionResult> GetRestaurantFood(Guid? restaurantId, Guid? foodId)
    {
        try
        {
            var restaurantFood = await _restaurantFoodLogic.GetRestaurantFoodAsync(restaurantId, foodId);
            if (restaurantFood == null)
            {
                return NotFound();
            }

            var result = new RestaurantFoodDto
            {
                Food_ID = restaurantFood.Food_ID,
                Restaurant_ID = restaurantFood.Restaurant_ID
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllRestaurantFoods()
    {
        try
        {
            var restaurantFoods = await _restaurantFoodLogic.GetAllRestaurantFoodsAsync();

            var result = restaurantFoods.Select(rf => new RestaurantFoodDto
            {
                Food_ID = rf.Food_ID,
                Restaurant_ID = rf.Restaurant_ID
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{restaurantId}/{foodId}")]
    public async Task<IActionResult> UpdateRestaurantFood(Guid? restaurantId, Guid? foodId, [FromBody] RestaurantFoodDto restaurantFoodDto)
    {
        if (restaurantFoodDto == null || !restaurantFoodDto.Food_ID.HasValue || !restaurantFoodDto.Restaurant_ID.HasValue)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            var restaurantFood = await _restaurantFoodLogic.GetRestaurantFoodAsync(restaurantId, foodId);
            if (restaurantFood == null)
            {
                return NotFound();
            }

            var restaurantExists = await _restaurantFoodLogic.RestaurantExistsAsync(restaurantFoodDto.Restaurant_ID);
            var foodExists = await _restaurantFoodLogic.FoodExistsAsync(restaurantFoodDto.Food_ID);

            if (!restaurantExists || !foodExists)
            {
                return NotFound("Restaurant or Food not found.");
            }

            restaurantFood.Restaurant_ID = restaurantFoodDto.Restaurant_ID.Value;
            restaurantFood.Food_ID = restaurantFoodDto.Food_ID.Value;

            await _restaurantFoodLogic.UpdateRestaurantFoodAsync(restaurantFood);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{restaurantId}/{foodId}")]
    public async Task<IActionResult> DeleteRestaurantFood(Guid? restaurantId, Guid? foodId)
    {
        try
        {
            await _restaurantFoodLogic.DeleteRestaurantFoodAsync(restaurantId, foodId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
