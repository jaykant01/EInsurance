using System.ComponentModel.DataAnnotations;

namespace EInsurance.Models.Entities
{
    public class InsurancePlan
    {
        [Key]
        public int PlanID { get; set; }

        [Required, MaxLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        public string PlanDetails { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Scheme> Schemes { get; set; } = new List<Scheme>();

    }
}
