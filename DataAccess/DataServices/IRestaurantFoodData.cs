using Core.Entiteti;

namespace DataAccess.DataServices
{
    public interface IRestaurantFoodData
    {
        Task AddRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task<Restaurant_Food> GetRestaurantFoodAsync(Guid? restaurantId, Guid? foodId);
        Task<IEnumerable<Restaurant_Food>> GetAllRestaurantFoodsAsync();
        Task<bool> RestaurantExistsAsync(Guid? restaurantId);
        Task<bool> FoodExistsAsync(Guid? foodId);
        Task UpdateRestaurantFoodAsync(Restaurant_Food restaurantFood);
        Task DeleteRestaurantFoodAsync(Guid? restaurantId, Guid? foodId);

    }
}
