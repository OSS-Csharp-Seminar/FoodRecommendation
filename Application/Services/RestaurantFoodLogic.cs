using Application.Interfaces;
using Core.Entiteti;
using DataAccess.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RestaurantFoodLogic : IRestaurantFoodLogic
    {
        private readonly IRestaurantFoodData _restaurantFoodData;

        public RestaurantFoodLogic(IRestaurantFoodData restaurantFoodData)
        {
            _restaurantFoodData = restaurantFoodData;
        }

        public async Task<List<Restaurant_Food>> GetAllRestaurantFoodsAsync()
        {
            return await _restaurantFoodData.GetAllAsync();
        }

        public async Task<Restaurant_Food> GetRestaurantFoodByIdAsync(Guid restaurantId, Guid foodId)
        {
            return await _restaurantFoodData.GetByIdAsync(restaurantId, foodId);
        }

        public async Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            await _restaurantFoodData.AddAsync(restaurantFood);
        }

        public async Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            await _restaurantFoodData.UpdateAsync(restaurantFood);
        }

        public async Task DeleteRestaurantFoodAsync(Guid restaurantId, Guid foodId)
        {
            await _restaurantFoodData.DeleteAsync(restaurantId, foodId);
        }
    }
}
