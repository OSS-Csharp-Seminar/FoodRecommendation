using Core.Entiteti;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class City : BaseEntity
{
    [Key]
    public Guid CityId { get; set; }
    public string? Name { get; set; }
    public string? City_code { get; set; }
    public string? County { get; set; }
    public string? Zip { get; set; }

    public virtual ICollection<Restaurant>? Restaurants { get; set; } = new List<Restaurant>();
    //public virtual ICollection<Restaurant> Restaurants { get; set; }
    //public List<Restaurant>? Restaurants { get; set; } = new List<Restaurant>();
}
