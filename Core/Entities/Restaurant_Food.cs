using Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Core.Entiteti
{
    public class Restaurant_Food
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Restaurant_ID { get; set; }
        public Guid Food_ID { get; set; }

        [ForeignKey("Restaurant_ID")]
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("Food_ID")]
        public virtual Food? Food { get; set; }
    }
}
