using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RestaurantFoodDto
    {
        public Guid? Food_ID { get; set; }
        public Guid? Restaurant_ID { get; set; }

    }
}
