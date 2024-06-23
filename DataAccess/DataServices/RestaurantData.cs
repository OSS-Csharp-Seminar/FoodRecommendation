using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DataAccess.Persistence;
using Application.DataServices;
using Core.Entiteti;

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
            return await _context.Restaurant.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(Guid id)
        {
            return await _context.Restaurant.FindAsync(id);
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurant.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurant.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurant.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Restaurant>> GetByNameAsync(string name)
        {
            return await _context.Restaurant
                .Where(r => r.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Restaurant>> GetByCityNameAsync(string cityName)
        {
            return await _context.Restaurant
                .Where(r => r.City.Name.Contains(cityName))
                .ToListAsync();
        }
    }
}
