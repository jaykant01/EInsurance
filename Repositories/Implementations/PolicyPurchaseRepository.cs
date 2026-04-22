using EInsurance.Data;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EInsurance.Models.DTOs.PolicyPurchaseDtos;

namespace EInsurance.Repositories.Implementations
{
    public class PolicyPurchaseRepository : IPolicyPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PolicyPurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Plans
        public async Task<List<AvailablePlanDto>> GetAllPlansAsync()
        {
            return await _context.InsurancePlans
                .Include(p => p.Schemes)
                .Select(p => new AvailablePlanDto
                {
                    PlanID = p.PlanID,
                    PlanName = p.PlanName,
                    PlanDetails = p.PlanDetails,
                    TotalSchemes = p.Schemes.Count
                })
                .ToListAsync();
        }

        //  Get Schemes Under a Plan
        public async Task<List<AvailableSchemeDto>> GetSchemesByPlanIdAsync(int planId)
        {
            return await _context.Schemes
                .Where(s => s.PlanID == planId)
                .Include(s => s.Plan)
                .Select(s => new AvailableSchemeDto
                {
                    SchemeID = s.SchemeID,
                    SchemeName = s.SchemeName,
                    SchemeDetails = s.SchemeDetails,
                    PlanName = s.Plan!.PlanName,
                    PlanID = s.PlanID
                })
                .ToListAsync();
        }

        // Get All Schemes 
        public async Task<List<AvailableSchemeDto>> GetAllSchemesAsync()
        {
            return await _context.Schemes
                .Include(s => s.Plan)
                .Select(s => new AvailableSchemeDto
                {
                    SchemeID = s.SchemeID,
                    SchemeName = s.SchemeName,
                    SchemeDetails = s.SchemeDetails,
                    PlanName = s.Plan!.PlanName,
                    PlanID = s.PlanID
                })
                .ToListAsync();
        }

        // Get Single Scheme
        public async Task<AvailableSchemeDto?> GetSchemeByIdAsync(int schemeId)
        {
            return await _context.Schemes
                .Where(s => s.SchemeID == schemeId)
                .Include(s => s.Plan)
                .Select(s => new AvailableSchemeDto
                {
                    SchemeID = s.SchemeID,
                    SchemeName = s.SchemeName,
                    SchemeDetails = s.SchemeDetails,
                    PlanName = s.Plan!.PlanName,
                    PlanID = s.PlanID
                })
                .FirstOrDefaultAsync();
        }

        //  Purchase Policy 
        public async Task<Policy> PurchasePolicyAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        //  Check Duplicate 
        public async Task<bool> CustomerHasSchemeAsync(int customerId, int schemeId)
        {
            return await _context.Policies
                .AnyAsync(p => p.CustomerID == customerId
                            && p.SchemeID == schemeId);
        }
    }
}
