using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DataServices;
using Application.Interfaces;
using Core.Entiteti;
using DataAccess.Repositories;

namespace Application.Services
{
    public class RestaurantLogic : IRestaurantLogic
    {
        public readonly IRestaurantDS _restaurantDS;

        public RestaurantLogic(IRestaurantDS restaurantDS)
        {
            _restaurantDS = restaurantDS;
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            return await _restaurantDS.GetAllAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(Guid id)
        {
            return await _restaurantDS.GetByIdAsync(id);
        }

        public async Task<List<Restaurant>> GetRestaurantsByNameAsync(string name)
        {
            return await _restaurantDS.GetByNameAsync(name);
        }

        public async Task<List<Restaurant>> GetRestaurantsByCityNameAsync(string cityName)
        {
            return await _restaurantDS.GetByCityNameAsync(cityName);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantDS.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantDS.UpdateAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(Guid id)
        {
            await _restaurantDS.DeleteAsync(id);
        }
    }
}
