using System.ComponentModel.DataAnnotations;

namespace EInsurance.Models.DTOs
{
    public class PolicyPurchaseDtos
    {
        // Show available plans to customer
        public class AvailablePlanDto
        {
            public int PlanID { get; set; }
            public string PlanName { get; set; } = string.Empty;
            public string PlanDetails { get; set; } = string.Empty;
            public int TotalSchemes { get; set; }
        }

        // Show schemes under a plan
        public class AvailableSchemeDto
        {
            public int SchemeID { get; set; }
            public string SchemeName { get; set; } = string.Empty;
            public string SchemeDetails { get; set; } = string.Empty;
            public string PlanName { get; set; } = string.Empty;
            public int PlanID { get; set; }
        }

        // Customer fills this form to purchase
        public class PurchasePolicyDto
        {
            [Required]
            public int SchemeID { get; set; }

            public string SchemeName { get; set; } = string.Empty;
            public string PlanName { get; set; } = string.Empty;

            [Required]
            public string PolicyDetails { get; set; } = string.Empty;

            [Required]
            [Range(1, 50, ErrorMessage = "Maturity period must be between 1 and 50 years.")]
            public int MaturityPeriod { get; set; }

            // Calculated fields — shown to customer before confirm
            public decimal Premium { get; set; }
            public DateTime DateIssued { get; set; }
            public DateTime PolicyLapseDate { get; set; }
        }
    }
}
