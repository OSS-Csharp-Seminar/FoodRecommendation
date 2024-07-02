using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataServices
{
    public interface ICityData
    {
        Task<List<City>> GetAllAsync();
        Task<City> GetByIdAsync(Guid id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(Guid id);
        Task<City> GetCityByNameAsync(string name);
        Task<City> GetCityByIdAsync(Guid id);
    }
}
