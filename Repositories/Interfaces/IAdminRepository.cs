using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;

namespace EInsurance.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        // Dashboard
        Task<DashboardStatsDto> GetDashboardStatsAsync();

        // Customers
        Task<PagedResultDto<CustomerListDto>> GetAllCustomersAsync(string search, int page, int pageSize);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);

        // Agents
        Task<PagedResultDto<AgentListDto>> GetAllAgentsAsync(string search, int page, int pageSize);
        Task<InsuranceAgent?> GetAgentByIdAsync(int id);
        Task<bool> UpdateAgentAsync(InsuranceAgent agent);
        Task<bool> DeleteAgentAsync(int id);

        // Employees
        Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(string search, int page, int pageSize);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
