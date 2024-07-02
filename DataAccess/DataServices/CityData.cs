using Core.Entiteti;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public class CityData : ICityData
    {
        private readonly DatabaseContext _context;

        public CityData(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.City.Include(c => c.Restaurants).ToListAsync();
        }

        public async Task<City> GetByIdAsync(Guid id)
        {
            return await _context.City.Include(c => c.Restaurants).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(City city)
        {
            await _context.City.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _context.City.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var city = await _context.City.FindAsync(id);
            if (city != null)
            {
                _context.City.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _context.City.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<City> GetCityByIdAsync(Guid id)
        {
            return await _context.City.FindAsync(id);
        }
    }
}
