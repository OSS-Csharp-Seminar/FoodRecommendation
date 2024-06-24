using Application.Interfaces;
using Core.Entiteti;
using DataAccess.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FoodCategoryLogic : IFoodCategoryLogic
    {
        private readonly IFoodCategoryData _foodCategoryData;

        public FoodCategoryLogic(IFoodCategoryData foodCategoryData)
        {
            _foodCategoryData = foodCategoryData;
        }

        public async Task<List<Food_category>> GetAllCategoriesAsync()
        {
            return await _foodCategoryData.GetAllAsync();
        }

        public async Task<Food_category> GetCategoryByIdAsync(Guid id)
        {
            return await _foodCategoryData.GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Food_category category)
        {
            await _foodCategoryData.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Food_category category)
        {
            await _foodCategoryData.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _foodCategoryData.DeleteAsync(id);
        }
    }
}
