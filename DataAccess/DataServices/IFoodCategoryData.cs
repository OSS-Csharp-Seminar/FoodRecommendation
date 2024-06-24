using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public interface IFoodCategoryData
    {
        Task<List<Food_category>> GetAllAsync();
        Task<Food_category> GetByIdAsync(Guid id);
        Task AddAsync(Food_category category);
        Task UpdateAsync(Food_category category);
        Task DeleteAsync(Guid id);
    }
}
