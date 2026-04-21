using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsurance.Models.Entities
{
    public class Policy
    {
        [Key]
        public int PolicyID { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        public int SchemeID { get; set; }

        [ForeignKey("SchemeID")]
        public Scheme? Scheme { get; set; }

        [Required]
        public string PolicyDetails { get; set; } = string.Empty;

        [Required]
        public Decimal Premium { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public int MaturityPeriod { get; set; }

        [Required]
        public DateTime PolicyLapseDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }
}
