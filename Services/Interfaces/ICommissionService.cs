using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface ICommissionService
    {
        // All agents summary
        Task<List<AgentCommissionSummaryDto>> GetAllAgentCommissionSummaryAsync();

        // Calculate commission for one agent
        Task<AgentCommissionDetailDto?> CalculateAgentCommissionAsync(
            int agentId, decimal commissionRate);

        // Save calculated commissions to DB
        Task<(bool Success, string Message)> SaveCommissionsAsync(
            int agentId, decimal commissionRate);
    }
}
