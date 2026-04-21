using System.ComponentModel.DataAnnotations;

namespace EInsurance.Models.Entities
{
    public class InsuranceAgent
    {
        [Key]
        public int AgentID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }
}
