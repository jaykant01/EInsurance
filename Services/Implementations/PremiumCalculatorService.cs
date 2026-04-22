using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Services.Implementations
{
    public class PremiumCalculatorService : IPremiumCalculatorService
    {
        private readonly AppDbContext _context;

        public PremiumCalculatorService(AppDbContext context)
        {
            _context = context;
        }

        //  Get Schemes For Dropdown 
        public async Task<List<SchemeSelectDto>> GetSchemesForDropdownAsync()
        {
            return await _context.Schemes
                .Include(s => s.Plan)
                .Select(s => new SchemeSelectDto
                {
                    SchemeID = s.SchemeID,
                    DisplayName = s.Plan!.PlanName + " → " + s.SchemeName
                })
                .ToListAsync();
        }

        // Main Premium Calculation 
        // Formula used:
        // Annual Premium = (SumAssured * RateOfInterest / 100)
        // Age Loading    = extra % based on age group
        // Final Annual   = Annual Premium + Age Loading amount
        // Monthly        = Final Annual / 12
        // Quarterly      = Final Annual / 4
        // Total Payable  = Final Annual * MaturityPeriod
        public PremiumCalculatorResultDto Calculate(PremiumCalculatorInputDto input)
        {
            // Step 1 — Base annual premium
            decimal basePremium = (input.SumAssured * input.RateOfInterest) / 100;

            // Step 2 — Age loading factor
            // Older customers pay more because of higher risk
            decimal ageLoadingPercent = input.Age switch
            {
                <= 25 => 0.00m,   // No loading for young customers
                <= 35 => 0.05m,   // 5% extra
                <= 45 => 0.10m,   // 10% extra
                <= 55 => 0.20m,   // 20% extra
                _ => 0.30m    // 30% extra for 56+
            };

            decimal ageLoading = basePremium * ageLoadingPercent;
            decimal annualPremium = Math.Round(basePremium + ageLoading, 2);

            // Step 3 — Other frequencies
            decimal monthlyPremium = Math.Round(annualPremium / 12, 2);
            decimal quarterlyPremium = Math.Round(annualPremium / 4, 2);

            // Step 4 — Total payable over maturity period
            decimal totalPayable = Math.Round(annualPremium * input.MaturityPeriod, 2);
            decimal totalInterest = Math.Round(totalPayable - input.SumAssured, 2);

            // Step 5 — Yearly breakdown
            var breakdown = new List<PremiumBreakdownDto>();
            decimal runningTotal = 0;

            for (int year = 1; year <= input.MaturityPeriod; year++)
            {
                runningTotal += annualPremium;

                // Compound interest on accumulated amount
                decimal interestEarned = Math.Round(
                    runningTotal * (input.RateOfInterest / 100), 2);

                breakdown.Add(new PremiumBreakdownDto
                {
                    Year = year,
                    PremiumPaid = annualPremium,
                    InterestEarned = interestEarned,
                    TotalValue = Math.Round(runningTotal + interestEarned, 2)
                });
            }

            return new PremiumCalculatorResultDto
            {
                // Echo inputs
                Age = input.Age,
                MaturityPeriod = input.MaturityPeriod,
                SumAssured = input.SumAssured,
                RateOfInterest = input.RateOfInterest,
                SchemeName = input.SchemeName,
                PlanName = input.PlanName,

                // Results
                AnnualPremium = annualPremium,
                MonthlyPremium = monthlyPremium,
                QuarterlyPremium = quarterlyPremium,
                TotalAmountPayable = totalPayable,
                TotalInterest = totalInterest,
                MaturityDate = DateTime.Now.AddYears(input.MaturityPeriod),

                // Breakdown
                YearlyBreakdown = breakdown
            };
        }
    }
}
