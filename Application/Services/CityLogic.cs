using Application.Interfaces;
using Core.Entiteti;
using DataAccess.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CityLogic : ICityLogic
    {
        private readonly ICityData _cityData;

        public CityLogic(ICityData cityData)
        {
            _cityData = cityData;
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _cityData.GetAllAsync();
        }

        public async Task<City> GetCityByIdAsync(Guid id)
        {
            return await _cityData.GetByIdAsync(id);
        }

        public async Task AddCityAsync(City city)
        {
            await _cityData.AddAsync(city);
        }

        public async Task UpdateCityAsync(City city)
        {
            await _cityData.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            await _cityData.DeleteAsync(id);
        }
    }
}
