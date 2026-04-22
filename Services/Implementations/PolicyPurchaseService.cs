using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;
using static EInsurance.Models.DTOs.PolicyPurchaseDtos;

namespace EInsurance.Services.Implementations
{
    public class PolicyPurchaseService : IPolicyPurchaseService
    {
        private readonly IPolicyPurchaseRepository _repo;

        public PolicyPurchaseService(IPolicyPurchaseRepository repo)
        {
            _repo = repo;
        }

        //  Browse Plans & Schemes 
        public async Task<List<AvailablePlanDto>> GetAllPlansAsync()
            => await _repo.GetAllPlansAsync();

        public async Task<List<AvailableSchemeDto>> GetAllSchemesAsync()
            => await _repo.GetAllSchemesAsync();

        public async Task<List<AvailableSchemeDto>> GetSchemesByPlanIdAsync(int planId)
            => await _repo.GetSchemesByPlanIdAsync(planId);

        public async Task<AvailableSchemeDto?> GetSchemeByIdAsync(int schemeId)
            => await _repo.GetSchemeByIdAsync(schemeId);

        //  Get Purchase Form with Pre-filled Data 
        public async Task<PurchasePolicyDto?> GetPurchaseFormAsync(
            int schemeId, int customerAge)
        {
            var scheme = await _repo.GetSchemeByIdAsync(schemeId);
            if (scheme == null) return null;

            return new PurchasePolicyDto
            {
                SchemeID = scheme.SchemeID,
                SchemeName = scheme.SchemeName,
                PlanName = scheme.PlanName,
                MaturityPeriod = 10,
                DateIssued = DateTime.Now,
            };
        }

        // Purchase Policy
        // Purchase policy — premium entered manually by customer
        public async Task<(bool Success, string Message)> PurchasePolicyAsync(
            PurchasePolicyDto dto, int customerId)
        {
            var alreadyHas = await _repo.CustomerHasSchemeAsync(
                customerId, dto.SchemeID);

            if (alreadyHas)
                return (false, "You already have a policy under this scheme.");

            var policy = new Policy
            {
                CustomerID = customerId,
                SchemeID = dto.SchemeID,
                PolicyDetails = dto.PolicyDetails,
                Premium = dto.Premium,
                DateIssued = DateTime.Now,
                MaturityPeriod = dto.MaturityPeriod,
                PolicyLapseDate = DateTime.Now.AddYears(dto.MaturityPeriod),
                CreatedAt = DateTime.Now
            };

            await _repo.PurchasePolicyAsync(policy);
            return (true, "Policy purchased successfully! 🎉");
        }
    }
}
