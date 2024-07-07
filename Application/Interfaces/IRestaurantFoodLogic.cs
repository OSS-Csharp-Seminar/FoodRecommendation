using Core.Entiteti;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRestaurantFoodLogic
    {
        Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task<Restaurant_Food> GetRestaurantFoodAsync(Guid? restaurantId, Guid? foodId);
        Task<IEnumerable<Restaurant_Food>> GetAllRestaurantFoodsAsync();
        Task<bool> RestaurantExistsAsync(Guid? restaurantId);
        Task<bool> FoodExistsAsync(Guid? foodId);
        Task<Restaurant> GetRestaurantAsync(string name, string city);
        Task<Food> GetFoodAsync(string name);
        Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task DeleteRestaurantFoodAsync(Guid? restaurantId, Guid? foodId);
    }
}
