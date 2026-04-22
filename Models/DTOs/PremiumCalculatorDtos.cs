using System.ComponentModel.DataAnnotations;

namespace EInsurance.Models.DTOs
{
    public class PremiumCalculatorInputDto
    {
        [Required]
        [Range(18, 70, ErrorMessage = "Age must be between 18 and 70.")]
        public int Age { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Maturity period must be between 1 and 50 years.")]
        public int MaturityPeriod { get; set; }

        [Required]
        [Range(10000, 10000000,
            ErrorMessage = "Sum assured must be between ₹10,000 and ₹1,00,00,000.")]
        public decimal SumAssured { get; set; }

        [Required]
        public int SchemeID { get; set; }

        [Required]
        [Range(0.1, 30, ErrorMessage = "Interest rate must be between 0.1% and 30%.")]
        public decimal RateOfInterest { get; set; }

        // Selected scheme/plan info — filled from dropdown
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
    }

    public class PremiumCalculatorResultDto
    {
        // Input echo
        public int Age { get; set; }
        public int MaturityPeriod { get; set; }
        public decimal SumAssured { get; set; }
        public decimal RateOfInterest { get; set; }
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;

        // Results
        public decimal AnnualPremium { get; set; }
        public decimal MonthlyPremium { get; set; }
        public decimal QuarterlyPremium { get; set; }
        public decimal TotalAmountPayable { get; set; }
        public decimal TotalInterest { get; set; }
        public DateTime MaturityDate { get; set; }

        // Breakdown
        public List<PremiumBreakdownDto> YearlyBreakdown { get; set; } = new();
    }

    public class PremiumBreakdownDto
    {
        public int Year { get; set; }
        public decimal PremiumPaid { get; set; }
        public decimal InterestEarned { get; set; }
        public decimal TotalValue { get; set; }
    }

    // For dropdown in calculator
    public class SchemeSelectDto
    {
        public int SchemeID { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
