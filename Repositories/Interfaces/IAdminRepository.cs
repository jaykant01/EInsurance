using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;

namespace EInsurance.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        // Dashboard
        Task<DashboardStatsDto> GetDashboardStatsAsync();

        // Customers
        Task<List<CustomerListDto>> GetAllCustomersAsync();
        Task<List<CustomerListDto>> SearchCustomersAsync(string search);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);

        // Agents
        Task<List<AgentListDto>> GetAllAgentsAsync();
        Task<List<AgentListDto>> SearchAgentsAsync(string search);
        Task<InsuranceAgent?> GetAgentByIdAsync(int id);
        Task<bool> UpdateAgentAsync(InsuranceAgent agent);
        Task<bool> DeleteAgentAsync(int id);

        // Employees
        Task<List<EmployeeListDto>> GetAllEmployeesAsync();
        Task<List<EmployeeListDto>> SearchEmployeesAsync(string search);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
