using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IAdminService
    {
        // Dashboard
        Task<DashboardStatsDto> GetDashboardStatsAsync();

        // Customers
        Task<List<CustomerListDto>> GetAllCustomersAsync();
        Task<List<CustomerListDto>> SearchCustomersAsync(string search);
        Task<EditCustomerDto?> GetCustomerForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateCustomerAsync(EditCustomerDto dto);
        Task<(bool Success, string Message)> DeleteCustomerAsync(int id);

        // Agents
        Task<List<AgentListDto>> GetAllAgentsAsync();
        Task<List<AgentListDto>> SearchAgentsAsync(string search);
        Task<EditAgentDto?> GetAgentForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateAgentAsync(EditAgentDto dto);
        Task<(bool Success, string Message)> DeleteAgentAsync(int id);

        // Employees
        Task<List<EmployeeListDto>> GetAllEmployeesAsync();
        Task<List<EmployeeListDto>> SearchEmployeesAsync(string search);
        Task<EditEmployeeDto?> GetEmployeeForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateEmployeeAsync(EditEmployeeDto dto);
        Task<(bool Success, string Message)> DeleteEmployeeAsync(int id);
    }
}
