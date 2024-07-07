using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CityDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? City_code { get; set; }
        public string? County { get; set; }
        public string? Zip { get; set; }
        public ICollection<RestaurantDto>? Restaurants { get; set; }
    }
}
