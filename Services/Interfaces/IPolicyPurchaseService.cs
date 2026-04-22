using static EInsurance.Models.DTOs.PolicyPurchaseDtos;

namespace EInsurance.Services.Interfaces
{
    public interface IPolicyPurchaseService
    {
        // Browse
        Task<List<AvailablePlanDto>> GetAllPlansAsync();
        Task<List<AvailableSchemeDto>> GetAllSchemesAsync();
        Task<List<AvailableSchemeDto>> GetSchemesByPlanIdAsync(int planId);
        Task<AvailableSchemeDto?> GetSchemeByIdAsync(int schemeId);

        // Purchase
        Task<PurchasePolicyDto?> GetPurchaseFormAsync(int schemeId, int customerAge);
        Task<(bool Success, string Message)> PurchasePolicyAsync(
            PurchasePolicyDto dto, int customerId);
    }
}
