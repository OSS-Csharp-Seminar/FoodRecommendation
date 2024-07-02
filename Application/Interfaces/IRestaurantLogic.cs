using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRestaurantLogic
    {
        Task<List<Restaurant>> GetRestaurantListAsync();
        Task<Restaurant> GetRestaurantByIdAsync(Guid id);
        Task<List<Restaurant>> GetRestaurantsByNameAsync(string name);
        Task<List<Restaurant>> GetRestaurantsByCityNameAsync(string cityName);
        Task AddRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(Guid id);
        Task<bool> RestaurantExistsAsync(string name);
    }
}