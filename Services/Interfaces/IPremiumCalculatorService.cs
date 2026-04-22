using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IPremiumCalculatorService
    {
        // Get schemes for dropdown
        Task<List<SchemeSelectDto>> GetSchemesForDropdownAsync();

        // Main calculation
        PremiumCalculatorResultDto Calculate(PremiumCalculatorInputDto input);
    }
}
