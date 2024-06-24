using Core.Entiteti;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICityLogic
    {
        Task<List<City>> GetAllCitiesAsync();
        Task<City> GetCityByIdAsync(Guid id);
        Task AddCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(Guid id);
    }
}
