using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RestaurantDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }

}
