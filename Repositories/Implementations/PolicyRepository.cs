using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Repositories.Implementations
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly AppDbContext _context;

        public PolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        // Customer Views Own Policies
        public async Task<List<CustomerPolicyDto>> GetCustomerPoliciesAsync(int customerId)
        {
            return await _context.Policies
                .Where(p => p.CustomerID == customerId)
                .Include(p => p.Scheme)
                    .ThenInclude(s => s!.Plan)
                .Include(p => p.Payments)
                .Select(p => new CustomerPolicyDto
                {
                    PolicyID = p.PolicyID,
                    SchemeName = p.Scheme!.SchemeName,
                    PlanName = p.Scheme.Plan!.PlanName,
                    PolicyDetails = p.PolicyDetails,
                    Premium = p.Premium,
                    DateIssued = p.DateIssued,
                    MaturityPeriod = p.MaturityPeriod,
                    PolicyLapseDate = p.PolicyLapseDate,
                    Payments = p.Payments
                        .Select(pay => new PaymentDetailDto
                        {
                            PaymentID = pay.PaymentID,
                            Amount = pay.Amount,
                            PaymentDate = pay.PaymentDate
                        }).ToList()
                })
                .ToListAsync();
        }

        // Admin Searches Customers
        public async Task<List<CustomerSearchDto>> SearchCustomersAsync(string searchTerm)
        {
            var query = _context.Customers
                .Include(c => c.Agent)
                .Include(c => c.Policies)
                .AsQueryable();

            // If search term provided filter by name or email
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(c =>
                    c.FullName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm));
            }

            return await query
                .Select(c => new CustomerSearchDto
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    AgentName = c.Agent != null ? c.Agent.FullName : "N/A",
                    TotalPolicies = c.Policies.Count
                })
                .ToListAsync();
        }

        // Admin Views Customer Specific Policies 
        public async Task<List<AdminCustomerPolicyDto>> GetPoliciesByCustomerIdAsync(int customerId)
        {
            return await _context.Policies
                .Where(p => p.CustomerID == customerId)
                .Include(p => p.Customer)
                .Include(p => p.Scheme)
                    .ThenInclude(s => s!.Plan)
                .Include(p => p.Payments)
                .Select(p => new AdminCustomerPolicyDto
                {
                    PolicyID = p.PolicyID,
                    CustomerName = p.Customer!.FullName,
                    CustomerEmail = p.Customer.Email,
                    SchemeName = p.Scheme!.SchemeName,
                    PlanName = p.Scheme.Plan!.PlanName,
                    PolicyDetails = p.PolicyDetails,
                    Premium = p.Premium,
                    DateIssued = p.DateIssued,
                    MaturityPeriod = p.MaturityPeriod,
                    PolicyLapseDate = p.PolicyLapseDate,
                    TotalPaid = p.Payments.Sum(pay => pay.Amount),
                    Payments = p.Payments
                        .Select(pay => new PaymentDetailDto
                        {
                            PaymentID = pay.PaymentID,
                            Amount = pay.Amount,
                            PaymentDate = pay.PaymentDate
                        }).ToList()
                })
                .ToListAsync();
        }
    }
}
