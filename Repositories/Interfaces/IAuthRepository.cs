using EInsurance.Models.Entities;

namespace EInsurance.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        // Login lookups
        Task<Admin> GetAdminByUsernameAsync(string username);
        Task<Employee?> GetEmployeeByUsernameAsync(string username);
        Task<InsuranceAgent?> GetAgentByUsernameAsync(string username);
        Task<Customer?> GetCustomerByEmailAsync(string email);

        // Customer self-registration
        Task<Customer> RegisterCustomerAsync(Customer customer);
        Task<bool> CustomerEmailExistsAsync(string email);

        // Admin creates Employee
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<bool> EmployeeUsernameExistsAsync(string username);
        Task<bool> EmployeeEmailExistsAsync(string email);

        // Admin creates Agent
        Task<InsuranceAgent> CreateAgentAsync(InsuranceAgent agent);
        Task<bool> AgentUsernameExistsAsync(string username);
        Task<bool> AgentEmailExistsAsync(string email);
    }
}
