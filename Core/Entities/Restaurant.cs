using Core.Common;

namespace Core.Entiteti
{
    public class Restaurant : BaseEntity
    {
        public Guid? CityId { get; set; } 
        public City? City { get; set; }   

        public string? Name { get; set; }
    }
}
