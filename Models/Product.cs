using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuotationSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public required string Code { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Cost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
