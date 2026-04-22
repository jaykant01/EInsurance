using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Repositories.Implementations
{
    public class CommissionRepository : ICommissionRepository
    {
        private readonly AppDbContext _context;

        public CommissionRepository(AppDbContext context)
        {
            _context = context;
        }

        // All Agents Commission Summary
        public async Task<List<AgentCommissionSummaryDto>> GetAllAgentCommissionSummaryAsync()
        {
            return await _context.InsuranceAgents
                .Include(a => a.Customers)
                    .ThenInclude(c => c.Policies)
                .Include(a => a.Commissions)
                .Select(a => new AgentCommissionSummaryDto
                {
                    AgentID = a.AgentID,
                    AgentName = a.FullName,
                    Email = a.Email,
                    TotalPoliciesSold = a.Customers
                        .SelectMany(c => c.Policies).Count(),
                    TotalCommissionEarned = a.Commissions
                        .Sum(c => c.CommissionAmount),
                    LastCommissionDate = a.Commissions
                        .OrderByDescending(c => c.CreatedAt)
                        .Select(c => (DateTime?)c.CreatedAt)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        // Agent Detail for Commission Calculation 
        public async Task<AgentCommissionDetailDto?> GetAgentCommissionDetailAsync(
            int agentId, decimal commissionRate)
        {
            var agent = await _context.InsuranceAgents
                .Include(a => a.Customers)
                    .ThenInclude(c => c.Policies)
                        .ThenInclude(p => p.Scheme)
                            .ThenInclude(s => s!.Plan)
                .FirstOrDefaultAsync(a => a.AgentID == agentId);

            if (agent == null) return null;

            // Get all policies sold through this agent's customers
            var policies = agent.Customers
                .SelectMany(c => c.Policies)
                .ToList();

            var policyCommissions = policies.Select(p => new PolicyCommissionDto
            {
                PolicyID = p.PolicyID,
                CustomerName = agent.Customers
                    .First(c => c.CustomerID == p.CustomerID).FullName,
                SchemeName = p.Scheme?.SchemeName ?? "N/A",
                PlanName = p.Scheme?.Plan?.PlanName ?? "N/A",
                Premium = p.Premium,
                CommissionAmount = Math.Round(
                    p.Premium * commissionRate / 100, 2),
                DateIssued = p.DateIssued
            }).ToList();

            return new AgentCommissionDetailDto
            {
                AgentID = agent.AgentID,
                AgentName = agent.FullName,
                Email = agent.Email,
                CommissionRate = commissionRate,
                TotalPoliciesSold = policies.Count,
                TotalPremiumGenerated = policies.Sum(p => p.Premium),
                TotalCommissionEarned = policyCommissions
                    .Sum(p => p.CommissionAmount),
                PolicyCommissions = policyCommissions
            };
        }

        //Save Commission Records
        public async Task SaveCommissionsAsync(List<Commission> commissions)
        {
            foreach (var commission in commissions)
            {
                var exists = await CommissionExistsForPolicyAsync(
                    commission.PolicyID, commission.AgentID);

                if (!exists)
                    _context.Commissions.Add(commission);
                else
                {
                    // Update existing
                    var existing = await _context.Commissions
                        .FirstAsync(c => c.PolicyID == commission.PolicyID
                                      && c.AgentID == commission.AgentID);
                    existing.CommissionAmount = commission.CommissionAmount;
                }
            }
            await _context.SaveChangesAsync();
        }

        //Check Duplicate
        public async Task<bool> CommissionExistsForPolicyAsync(
            int policyId, int agentId)
        {
            return await _context.Commissions
                .AnyAsync(c => c.PolicyID == policyId
                            && c.AgentID == agentId);
        }
    }
}
