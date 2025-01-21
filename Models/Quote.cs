using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuotationSystem.Models
{
    public class Quote
    {
        [Key]
        public int QuoteID { get; set; }

        [MaxLength(50)]
        public required string? Client { get; set; }

        [MaxLength(50)]
        public required string? PhoneNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }

        public DateTime? Date { get; set; }
    }
}
