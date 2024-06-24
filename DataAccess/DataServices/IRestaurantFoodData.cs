using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public interface IRestaurantFoodData
    {
        Task<List<Restaurant_Food>> GetAllAsync();
        Task<Restaurant_Food> GetByIdAsync(Guid restaurantId, Guid foodId);
        Task AddAsync(Restaurant_Food restaurantFood);
        Task UpdateAsync(Restaurant_Food restaurantFood);
        Task DeleteAsync(Guid restaurantId, Guid foodId);
    }
}
