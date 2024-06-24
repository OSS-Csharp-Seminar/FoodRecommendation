using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFoodLogic
    {
        Task<List<Food>> GetAllFoodsAsync();
        Task<Food> GetFoodByIdAsync(Guid id);
        Task AddFoodAsync(Food food);
        Task UpdateFoodAsync(Food food);
        Task DeleteFoodAsync(Guid id);
    }
}
