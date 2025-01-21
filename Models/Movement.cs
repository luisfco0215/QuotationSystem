using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuotationSystem.Models
{
    public class Movement
    {
        [Key]
        public int MovementID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product? Product { get; set; }

        [Required]
        public int QuoteID { get; set; }

        [ForeignKey(nameof(QuoteID))]
        public Quote? Quote { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
    }
}
