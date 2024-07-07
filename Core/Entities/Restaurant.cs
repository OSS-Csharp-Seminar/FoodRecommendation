using Core.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entiteti
{
    public class Restaurant : BaseEntity
    {
        public string? Name { get; set; }
        [ForeignKey("CityId")]
        public Guid? Id { get; set; }
        public Guid? CityId { get; set; }

        [JsonIgnore]
        public City? City { get; set; }


        public ICollection<Restaurant_Food>? Restaurant_Foods { get; set; }
        //public City? City { get; set; }   

    }
}
