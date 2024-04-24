using Core.Common;

namespace Core.Entiteti
{
    public class Food : BaseEntity
    {
        public Food_category Category_ID;
        public string Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
