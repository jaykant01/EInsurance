using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IAdminService
    {
        // Dashboard
        Task<DashboardStatsDto> GetDashboardStatsAsync();

        // Customers
        Task<PagedResultDto<CustomerListDto>> GetAllCustomersAsync(string search, int page, int pageSize);
        Task<EditCustomerDto?> GetCustomerForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateCustomerAsync(EditCustomerDto dto);
        Task<(bool Success, string Message)> DeleteCustomerAsync(int id);

        // Agents
        Task<PagedResultDto<AgentListDto>> GetAllAgentsAsync(string search, int page, int pageSize);
        Task<EditAgentDto?> GetAgentForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateAgentAsync(EditAgentDto dto);
        Task<(bool Success, string Message)> DeleteAgentAsync(int id);

        // Employees
        Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(string search, int page, int pageSize);
        Task<EditEmployeeDto?> GetEmployeeForEditAsync(int id);
        Task<(bool Success, string Message)> UpdateEmployeeAsync(EditEmployeeDto dto);
        Task<(bool Success, string Message)> DeleteEmployeeAsync(int id);
    }
}
