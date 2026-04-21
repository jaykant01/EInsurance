using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IPolicyService
    {
        // Customer
        Task<List<CustomerPolicyDto>> GetMyPoliciesAsync(int customerId);

        // Admin
        Task<List<CustomerSearchDto>> SearchCustomersAsync(string searchTerm);
        Task<List<AdminCustomerPolicyDto>> GetCustomerPoliciesAsync(int customerId);
    }
}
