using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RestaurantDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public Guid? CityId { get; set; }
        public string City { get; set; }

        public List<RestaurantFoodDto>? Restaurant_Foods { get; set; }
    }

}
