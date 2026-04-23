using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Repositories.Implementations
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Customer Payments
        public async Task<List<CustomerPaymentListDto>> GetCustomerPaymentsAsync(
            int customerId)
        {
            return await _context.Payments
                .Where(p => p.CustomerID == customerId)
                .Include(p => p.Policy)
                    .ThenInclude(p => p!.Scheme)
                        .ThenInclude(s => s!.Plan)
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => new CustomerPaymentListDto
                {
                    PaymentID = p.PaymentID,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PolicyID = p.PolicyID,
                    SchemeName = p.Policy!.Scheme!.SchemeName,
                    PlanName = p.Policy.Scheme.Plan!.PlanName,
                    ReceiptNumber = "RCPT-" + p.PaymentID.ToString("D6")
                })
                .ToListAsync();
        }

        // Get Single Payment for Receipt 
        public async Task<PaymentReceiptDto?> GetPaymentReceiptAsync(
            int paymentId, int customerId)
        {
            var payment = await _context.Payments
                .Where(p => p.PaymentID == paymentId
                         && p.CustomerID == customerId)
                .Include(p => p.Customer)
                    .ThenInclude(c => c!.Agent)
                .Include(p => p.Policy)
                    .ThenInclude(p => p!.Scheme)
                        .ThenInclude(s => s!.Plan)
                .FirstOrDefaultAsync();

            if (payment == null) return null;

            return new PaymentReceiptDto
            {
                // Receipt
                PaymentID = payment.PaymentID,
                ReceiptNumber = "RCPT-" + payment.PaymentID.ToString("D6"),
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,

                // Customer
                CustomerID = payment.CustomerID,
                CustomerName = payment.Customer!.FullName,
                CustomerEmail = payment.Customer.Email,
                CustomerPhone = payment.Customer.Phone,

                // Policy
                PolicyID = payment.PolicyID,
                PolicyDetails = payment.Policy!.PolicyDetails,
                SchemeName = payment.Policy.Scheme!.SchemeName,
                PlanName = payment.Policy.Scheme.Plan!.PlanName,
                Premium = payment.Policy.Premium,
                DateIssued = payment.Policy.DateIssued,
                PolicyLapseDate = payment.Policy.PolicyLapseDate,

                // Agent
                AgentName = payment.Customer.Agent != null
                    ? payment.Customer.Agent.FullName
                    : "N/A"
            };
        }
    }
}
