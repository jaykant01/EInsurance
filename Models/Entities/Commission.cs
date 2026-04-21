using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsurance.Models.Entities
{
    public class Commission
    {
        [Key]
        public int CommissionID { get; set; }

        public int AgentID { get; set; }
        [ForeignKey("AgentID")]
        public InsuranceAgent? Agent { get; set; }
        public int PolicyID { get; set; }
        [ForeignKey("PolicyID")]
        public Policy? Policy { get; set; }

        [Required]
        public decimal CommissionAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
