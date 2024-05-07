namespace Core.Common
{
    public interface IAuditedEntity
    {
        public string Price { get; set; }
        public string RestaurantName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
