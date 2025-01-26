using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merjane.Entities
{
    [Table("products")]
    public class Product : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("lead_time")]
        public int LeadTime { get; set; }

        [Column("available")]
        public int Available { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("season_start_date")]
        public DateTime? SeasonStartDate { get; set; }

        [Column("season_end_date")]
        public DateTime? SeasonEndDate { get; set; }

        public long? OrderId { get; set; }

        [ForeignKey("OrderId")] // Specifies that this property is a foreign key
        public Order? Order { get; set; }
    }

}
