using Application.Interfaces;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        private readonly IFoodCategoryLogic _foodCategoryLogic;

        public FoodCategoryController(IFoodCategoryLogic foodCategoryLogic)
        {
            _foodCategoryLogic = foodCategoryLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFoodCategories()
        {
            try
            {
                var categories = await _foodCategoryLogic.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodCategory(Guid id)
        {
            try
            {
                var category = await _foodCategoryLogic.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<Food_category>> PostFoodCategory(Food_category category)
        {
            try
            {
                await _foodCategoryLogic.AddCategoryAsync(category);
                return CreatedAtAction("GetFoodCategory", new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodCategory(Guid id, Food_category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest();
                }

                await _foodCategoryLogic.UpdateCategoryAsync(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodCategory(Guid id)
        {
            try
            {
                var category = await _foodCategoryLogic.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                await _foodCategoryLogic.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
