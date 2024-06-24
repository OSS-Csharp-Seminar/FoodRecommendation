using Application.Interfaces;
using Core.Entiteti;
using DataAccess.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FoodLogic : IFoodLogic
    {
        private readonly IFoodData _foodData;

        public FoodLogic(IFoodData foodData)
        {
            _foodData = foodData;
        }

        public async Task<List<Food>> GetAllFoodsAsync()
        {
            return await _foodData.GetAllAsync();
        }

        public async Task<Food> GetFoodByIdAsync(Guid id)
        {
            return await _foodData.GetByIdAsync(id);
        }

        public async Task AddFoodAsync(Food food)
        {
            await _foodData.AddAsync(food);
        }

        public async Task UpdateFoodAsync(Food food)
        {
            await _foodData.UpdateAsync(food);
        }

        public async Task DeleteFoodAsync(Guid id)
        {
            await _foodData.DeleteAsync(id);
        }
    }
}
