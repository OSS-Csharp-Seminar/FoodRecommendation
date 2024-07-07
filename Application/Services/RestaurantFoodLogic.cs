using Application.DataServices;
using Application.Interfaces;
using Core.Entiteti;
using DataAccess.DataServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RestaurantFoodLogic : IRestaurantFoodLogic
    {
        private readonly IRestaurantFoodData _restaurantFoodData;
        private readonly IRestaurantDS _restaurantData;
        private readonly IFoodData _foodData;

        public RestaurantFoodLogic(IRestaurantFoodData restaurantFoodData, IRestaurantDS restaurantData, IFoodData foodData)
        {
            _restaurantFoodData = restaurantFoodData;
            _restaurantData = restaurantData;
            _foodData = foodData;
        }

        public async Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            var existingLink = await _restaurantFoodData.GetRestaurantFoodAsync(restaurantFood.Restaurant_ID, restaurantFood.Food_ID);

            if (existingLink == null)
            {
                await _restaurantFoodData.AddRestaurantFoodAsync(restaurantFood);
            }
        }

        public async Task<Restaurant_Food> GetRestaurantFoodAsync(Guid? restaurantId, Guid? foodId)
        {
            return await _restaurantFoodData.GetRestaurantFoodAsync(restaurantId, foodId);
        }

        public async Task<IEnumerable<Restaurant_Food>> GetAllRestaurantFoodsAsync()
        {
            return await _restaurantFoodData.GetAllRestaurantFoodsAsync();
        }

        public async Task<bool> RestaurantExistsAsync(Guid? restaurantId)
        {
            return await _restaurantFoodData.RestaurantExistsAsync(restaurantId);
        }

        public async Task<bool> FoodExistsAsync(Guid? foodId)
        {
            return await _restaurantFoodData.FoodExistsAsync(foodId);
        }

        public async Task<Restaurant> GetRestaurantAsync(string name, string city)
        {
            return await _restaurantData.GetByNameAndCityAsync(name, city);
        }

        public async Task<Food> GetFoodAsync(string name)
        {
            return await _foodData.GetByNameAsync(name);
        }

        public async Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood)
        {
            await _restaurantFoodData.UpdateRestaurantFoodAsync(restaurantFood);
        }

        public async Task DeleteRestaurantFoodAsync(Guid? restaurantId, Guid? foodId)
        {
            await _restaurantFoodData.DeleteRestaurantFoodAsync(restaurantId, foodId);
        }
    }
}
