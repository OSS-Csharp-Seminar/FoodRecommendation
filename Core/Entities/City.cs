using Core.Entiteti;
using System.Text.RegularExpressions;

public class City : BaseEntity
{
    public string? Name { get; set; }
    public string? City_code { get; set; }
    public string? County { get; set; }
    public string? Zip { get; set; }
    public virtual ICollection<Restaurant> Restaurants { get; set; }
    //public List<Restaurant>? Restaurants { get; set; } = new List<Restaurant>();
}
