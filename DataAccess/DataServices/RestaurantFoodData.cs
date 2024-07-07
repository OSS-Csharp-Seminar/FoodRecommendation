using Core.Entiteti;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            var existingLink = await _context.Restaurant_Food
                .FirstOrDefaultAsync(rf => rf.Restaurant_ID == restaurantFood.Restaurant_ID && rf.Food_ID == restaurantFood.Food_ID);

            if (existingLink == null)
            {
                _context.Restaurant_Food.Add(restaurantFood);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Restaurant_Food> GetRestaurantFoodAsync(Guid? restaurantId, Guid? foodId)
        {
            return await _context.Restaurant_Food
                .Include(rf => rf.Food)
                .Include(rf => rf.Restaurant)
                .ThenInclude(r => r.City)  
                .FirstOrDefaultAsync(rf => rf.Restaurant_ID == restaurantId && rf.Food_ID == foodId);
        }

        public async Task<IEnumerable<Restaurant_Food>> GetAllRestaurantFoodsAsync()
        {
            return await _context.Restaurant_Food
                .Include(rf => rf.Food)
                .Include(rf => rf.Restaurant)
                .ThenInclude(r => r.City)
                .ToListAsync();
        }

        public async Task<bool> RestaurantExistsAsync(Guid? restaurantId)
        {
            return await _context.Restaurant.AnyAsync(r => r.Id == restaurantId);
        }

        public async Task<bool> FoodExistsAsync(Guid? foodId)
        {
            return await _context.Food.AnyAsync(f => f.Id == foodId);
        }

        public async Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            _context.Restaurant_Food.Update(restaurantFood);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRestaurantFoodAsync(Guid? restaurantId, Guid? foodId)
        {
            var restaurantFood = await GetRestaurantFoodAsync(restaurantId, foodId);
            if (restaurantFood != null)
            {
                _context.Restaurant_Food.Remove(restaurantFood);
                await _context.SaveChangesAsync();
            }
        }
    }
}
