using Application.Interfaces;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodLogic _foodLogic;

        public FoodController(IFoodLogic foodLogic)
        {
            _foodLogic = foodLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFoods()
        {
            try
            {
                var foods = await _foodLogic.GetAllFoodsAsync();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(Guid id)
        {
            try
            {
                var food = await _foodLogic.GetFoodByIdAsync(id);
                if (food == null)
                {
                    return NotFound();
                }

                return Ok(food);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            try
            {
                await _foodLogic.AddFoodAsync(food);
                return CreatedAtAction("GetFood", new { id = food.Id }, food);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(Guid id, Food food)
        {
            try
            {
                if (id != food.Id)
                {
                    return BadRequest();
                }

                await _foodLogic.UpdateFoodAsync(food);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            try
            {
                var food = await _foodLogic.GetFoodByIdAsync(id);
                if (food == null)
                {
                    return NotFound();
                }

                await _foodLogic.DeleteFoodAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
