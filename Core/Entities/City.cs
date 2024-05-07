using Core.Common;

namespace Core.Entiteti
{
    public class City :BaseEntity
    {
        public string Name{ get;set;}
        public string ZIP { get; set; }
        public string County { get; set; }
        public string City_code { get; set; }
    }
}
