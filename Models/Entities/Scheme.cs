using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsurance.Models.Entities
{
    public class Scheme
    {
        [Key]
        public int SchemeID { get; set; }

        [Required, MaxLength(100)]
        public string SchemeName { get; set; } = string.Empty;

        [Required]
        public string SchemeDetails { get; set; } = string.Empty;

        public int PlanID { get; set; }

        [ForeignKey("PlanID")]
        public InsurancePlan? Plan { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
        public ICollection<EmployeeScheme> EmployeeSchemes { get; set; } = new List<EmployeeScheme>();

    }
}
