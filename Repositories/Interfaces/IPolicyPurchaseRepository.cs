using EInsurance.Models.Entities;
using static EInsurance.Models.DTOs.PolicyPurchaseDtos;

namespace EInsurance.Repositories.Interfaces
{
    public interface IPolicyPurchaseRepository
    {
        // Get all plans
        Task<List<AvailablePlanDto>> GetAllPlansAsync();

        // Get schemes under a plan
        Task<List<AvailableSchemeDto>> GetSchemesByPlanIdAsync(int planId);

        // Get all schemes
        Task<List<AvailableSchemeDto>> GetAllSchemesAsync();

        // Get single scheme
        Task<AvailableSchemeDto?> GetSchemeByIdAsync(int schemeId);

        // Purchase policy
        Task<Policy> PurchasePolicyAsync(Policy policy);

        // Check if customer already has this scheme
        Task<bool> CustomerHasSchemeAsync(int customerId, int schemeId);
    }
}
