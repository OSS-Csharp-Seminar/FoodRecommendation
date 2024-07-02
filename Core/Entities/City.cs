using Core.Entiteti;

public class City : BaseEntity
{
    public string? Name { get; set; }
    public string? City_code { get; set; }
    public string? County { get; set; }
    public string? Zip { get; set; }
    public List<Restaurant>? Restaurants { get; set; } = new List<Restaurant>();
}
