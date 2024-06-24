using Application.Interfaces;
using Core.Entiteti;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityLogic _cityLogic;

        public CityController(ICityLogic cityLogic)
        {
            _cityLogic = cityLogic;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                var cities = await _cityLogic.GetAllCitiesAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            try
            {
                var city = await _cityLogic.GetCityByIdAsync(id);
                if (city == null)
                {
                    return NotFound();
                }
                return Ok(city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCity(City city)
        {
            try
            {
                if (city == null)
                {
                    return BadRequest("City object is null");
                }

                await _cityLogic.AddCityAsync(city);
                return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(Guid id, City city)
        {
            try
            {
                if (id != city.Id)
                {
                    return BadRequest();
                }

                await _cityLogic.UpdateCityAsync(city);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            try
            {
                await _cityLogic.DeleteCityAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
