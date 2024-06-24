using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFoodCategoryLogic
    {
        Task<List<Food_category>> GetAllCategoriesAsync();
        Task<Food_category> GetCategoryByIdAsync(Guid id);
        Task AddCategoryAsync(Food_category category);
        Task UpdateCategoryAsync(Food_category category);
        Task DeleteCategoryAsync(Guid id);
    }
}
