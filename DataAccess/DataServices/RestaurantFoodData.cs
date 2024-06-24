using Core.Entiteti;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public class RestaurantFoodData : IRestaurantFoodData
    {
        private readonly DatabaseContext _context;

        public RestaurantFoodData(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant_Food>> GetAllAsync()
        {
            return await _context.Restaurant_Food.ToListAsync();
        }

        public async Task<Restaurant_Food> GetByIdAsync(Guid restaurantId, Guid foodId)
        {
            return await _context.Restaurant_Food.FindAsync(restaurantId, foodId);
        }

        public async Task AddAsync(Restaurant_Food restaurantFood)
        {
            await _context.Restaurant_Food.AddAsync(restaurantFood);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant_Food restaurantFood)
        {
            _context.Restaurant_Food.Update(restaurantFood);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid restaurantId, Guid foodId)
        {
            var restaurantFood = await _context.Restaurant_Food.FindAsync(restaurantId, foodId);
            if (restaurantFood != null)
            {
                _context.Restaurant_Food.Remove(restaurantFood);
                await _context.SaveChangesAsync();
            }
        }
    }
}
