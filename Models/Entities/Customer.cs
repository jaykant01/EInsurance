using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsurance.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public int AgentID { get; set; }

        [ForeignKey("AgentID")]
        public InsuranceAgent? Agent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
