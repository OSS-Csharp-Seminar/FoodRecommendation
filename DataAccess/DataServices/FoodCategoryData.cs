using Core.Entiteti;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public class FoodCategoryData : IFoodCategoryData
    {
        private readonly DatabaseContext _context;

        public FoodCategoryData(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Food_category>> GetAllAsync()
        {
            return await _context.Food_category.ToListAsync();
        }

        public async Task<Food_category> GetByIdAsync(Guid id)
        {
            return await _context.Food_category.FindAsync(id);
        }

        public async Task AddAsync(Food_category category)
        {
            await _context.Food_category.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Food_category category)
        {
            _context.Food_category.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Food_category.FindAsync(id);
            if (category != null)
            {
                _context.Food_category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
