using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;

namespace EInsurance.Services.Implementations
{
    public class CommissionService : ICommissionService
    {
        private readonly ICommissionRepository _commissionRepo;

        public CommissionService(ICommissionRepository commissionRepo)
        {
            _commissionRepo = commissionRepo;
        }

        // All Agents Summary 
        public async Task<List<AgentCommissionSummaryDto>> GetAllAgentCommissionSummaryAsync()
            => await _commissionRepo.GetAllAgentCommissionSummaryAsync();

        //  Calculate Commission 
        public async Task<AgentCommissionDetailDto?> CalculateAgentCommissionAsync(
            int agentId, decimal commissionRate)
            => await _commissionRepo.GetAgentCommissionDetailAsync(
                agentId, commissionRate);

        // Save Commissions
        public async Task<(bool Success, string Message)> SaveCommissionsAsync(
            int agentId, decimal commissionRate)
        {
            var detail = await _commissionRepo.GetAgentCommissionDetailAsync(
                agentId, commissionRate);

            if (detail == null)
                return (false, "Agent not found.");

            if (!detail.PolicyCommissions.Any())
                return (false, "No policies found for this agent.");

            // Build Commission entities
            var commissions = detail.PolicyCommissions.Select(p =>
                new Commission
                {
                    AgentID = agentId,
                    PolicyID = p.PolicyID,
                    CommissionAmount = p.CommissionAmount,
                    CreatedAt = DateTime.Now
                }).ToList();

            await _commissionRepo.SaveCommissionsAsync(commissions);

            return (true,
                $"Commission saved for {detail.TotalPoliciesSold} " +
                $"policies. Total: ₹{detail.TotalCommissionEarned:N2}");
        }
    }
}
