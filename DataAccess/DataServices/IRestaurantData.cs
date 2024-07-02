using Core.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataServices
{
    public interface IRestaurantDS
    {
        Task<List<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(Guid id);
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Guid id);
        Task<List<Restaurant>> GetByNameAsync(string name);
        Task<List<Restaurant>> GetByCityNameAsync(string cityName);
        Task<bool> RestaurantExistsAsync(string name);
    }
}
