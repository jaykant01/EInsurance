using EInsurance.Models.DTOs;

namespace EInsurance.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        // Customer — get own policies with payments
        Task<List<CustomerPolicyDto>> GetCustomerPoliciesAsync(int customerId);

        // Admin — search customers
        Task<List<CustomerSearchDto>> SearchCustomersAsync(string searchTerm);

        // Admin — get specific customer policies
        Task<List<AdminCustomerPolicyDto>> GetPoliciesByCustomerIdAsync(int customerId);
    }
}
