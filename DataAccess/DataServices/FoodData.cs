using Core.Entiteti;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public class FoodData : IFoodData
    {
        private readonly DatabaseContext _context;

        public FoodData(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetAllAsync()
        {
            return await _context.Food.ToListAsync();
        }

        public async Task<Food> GetByIdAsync(Guid id)
        {
            return await _context.Food.FindAsync(id);
        }

        public async Task AddAsync(Food food)
        {
            await _context.Food.AddAsync(food);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Food food)
        {
            _context.Food.Update(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food != null)
            {
                _context.Food.Remove(food);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Food> GetByNameAsync(string name)
        {
            return await _context.Food
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Name == name);
        }
    }
}
