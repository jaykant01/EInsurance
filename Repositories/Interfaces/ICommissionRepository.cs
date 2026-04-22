using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;

namespace EInsurance.Repositories.Interfaces
{
    public interface ICommissionRepository
    {
        // Get all agents with commission summary
        Task<List<AgentCommissionSummaryDto>> GetAllAgentCommissionSummaryAsync();

        // Get agent details for commission calculation
        Task<AgentCommissionDetailDto?> GetAgentCommissionDetailAsync(
            int agentId, decimal commissionRate);

        // Save commission records
        Task SaveCommissionsAsync(List<Commission> commissions);

        // Check if commission already saved for policy
        Task<bool> CommissionExistsForPolicyAsync(int policyId, int agentId);
    }
}
