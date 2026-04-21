using EInsurance.Models.DTOs;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;

namespace EInsurance.Services.Implementations
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepo;

        public PolicyService(IPolicyRepository policyRepo)
        {
            _policyRepo = policyRepo;
        }

        // Customer views own policies
        public async Task<List<CustomerPolicyDto>> GetMyPoliciesAsync(int customerId)
            => await _policyRepo.GetCustomerPoliciesAsync(customerId);

        // Admin searches customers
        public async Task<List<CustomerSearchDto>> SearchCustomersAsync(string searchTerm)
            => await _policyRepo.SearchCustomersAsync(searchTerm);

        // Admin views customer specific policies
        public async Task<List<AdminCustomerPolicyDto>> GetCustomerPoliciesAsync(int customerId)
            => await _policyRepo.GetPoliciesByCustomerIdAsync(customerId);
    }
}
