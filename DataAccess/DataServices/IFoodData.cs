using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public interface IFoodData
    {
        Task<List<Food>> GetAllAsync();
        Task<Food> GetByIdAsync(Guid id);
        Task AddAsync(Food food);
        Task UpdateAsync(Food food);
        Task DeleteAsync(Guid id);
    }
}
