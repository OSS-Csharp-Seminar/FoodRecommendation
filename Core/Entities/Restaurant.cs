using Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entiteti
{
    public class Restaurant : BaseEntity
    {
        [ForeignKey("CityId")]
        public Guid? CityId { get; set; }
        public virtual City? City { get; set; }
        //public City? City { get; set; }   

        public string? Name { get; set; }
    }
}
