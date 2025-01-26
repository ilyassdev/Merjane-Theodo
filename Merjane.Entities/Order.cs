using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Merjane.Entities
{
    [Table("orders")]
    public class Order : BaseEntity
    {
        //The entity keys structure is defined in the context
        public long Id { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
