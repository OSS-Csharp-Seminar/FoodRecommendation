using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DataAccess.Persistence;
using Application.DataServices;
using Core.Entiteti;
using Core.Exceptions;

namespace DataAccess.Repositories
{
    public class RestaurantDS : IRestaurantDS
    {
        private readonly DatabaseContext _context;

        public RestaurantDS(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurant.Include(r => r.CityId).ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(Guid id)
        {
            var restaurant = await _context.Restaurant.Include(r => r.CityId).FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with ID {id} was not found.");
            }

            return restaurant;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            try
            {
                await _context.Restaurant.AddAsync(restaurant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while adding the restaurant.", ex);
            }
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            try
            {
                _context.Restaurant.Update(restaurant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while updating the restaurant.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with ID {id} was not found.");
            }

            try
            {
                _context.Restaurant.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while deleting the restaurant.", ex);
            }
        }

        public async Task<List<Restaurant>> GetByNameAsync(string name)
        {
            try
            {
                return await _context.Restaurant
                    .Where(r => r.Name.Contains(name))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("An error occurred while retrieving restaurants by name.", ex);
            }
        }

        public async Task<List<Restaurant>> GetByCityNameAsync(string cityName)
        {
            var city = await _context.City
        .Where(c => c.Name.Contains(cityName))
        .FirstOrDefaultAsync();
            if (city == null)
            {
                // Handle the case when the city does not exist
                return new List<Restaurant>();
            }
            try
            {
                return await _context.Restaurant
        .Where(r => r.CityId == city.Id)
        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("An error occurred while retrieving restaurants by city name.", ex);
            }
        }
        public async Task<bool> RestaurantExistsAsync(string name)
        {
            return await _context.Restaurant.AnyAsync(r => r.Name == name);
        }
    }
}
    

