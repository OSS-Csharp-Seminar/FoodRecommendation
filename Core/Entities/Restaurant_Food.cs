using Core.Common;
namespace Core.Entiteti
{
    public class Restaurant_Food
    {
        public Guid? Food_ID;
        public Guid? Restaurant_ID;

        public Food? Food { get; set; }
        public Restaurant? Restaurant { get; set;}
    }
}
