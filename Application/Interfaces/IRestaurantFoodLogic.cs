using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRestaurantFoodLogic
    {
        Task<List<Restaurant_Food>> GetAllRestaurantFoodsAsync();
        Task<Restaurant_Food> GetRestaurantFoodByIdAsync(Guid restaurantId, Guid foodId);
        Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task DeleteRestaurantFoodAsync(Guid restaurantId, Guid foodId);
    }
}
