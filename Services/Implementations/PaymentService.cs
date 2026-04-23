using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace EInsurance.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PaymentService(
            AppDbContext context,
            IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //  Create Razorpay Order
        public async Task<RazorpayOrderDto?> CreateOrderAsync(
            int policyId, int customerId)
        {
            // Get policy with scheme and plan
            var policy = await _context.Policies
                .Include(p => p.Scheme)
                    .ThenInclude(s => s!.Plan)
                .FirstOrDefaultAsync(p => p.PolicyID == policyId);

            if (policy == null) return null;

            // Get customer
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == customerId);

            if (customer == null) return null;

            var keyId = _config["Razorpay:KeyId"]!;
            var keySecret = _config["Razorpay:KeySecret"]!;

            // Amount in paise (multiply by 100)
            var amountInPaise = (int)(policy.Premium * 100);

            // Create Razorpay client
            var client = new RazorpayClient(keyId, keySecret);

            // Create order
            var options = new Dictionary<string, object>
            {
                { "amount",   amountInPaise },
                { "currency", "INR" },
                { "receipt",  "policy_" + policyId + "_" +
                              DateTime.Now.Ticks },
                { "notes", new Dictionary<string, string>
                    {
                        { "policy_id",   policyId.ToString() },
                        { "customer_id", customerId.ToString() }
                    }
                }
            };

            var order = client.Order.Create(options);

            return new RazorpayOrderDto
            {
                OrderId = order["id"].ToString()!,
                Amount = policy.Premium,
                Currency = "INR",
                KeyId = keyId,
                CustomerName = customer.FullName,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.Phone,
                PolicyDescription = policy.Scheme?.Plan?.PlanName +
                                    " → " +
                                    policy.Scheme?.SchemeName,
                PolicyID = policyId
            };
        }

        //  Verify Payment Signature & Save 
        public async Task<(bool Success, string Message)>
            VerifyAndSavePaymentAsync(PaymentVerificationDto dto)
        {
            // Step 1 — Verify Razorpay signature
            var keySecret = _config["Razorpay:KeySecret"]!;
            var isValid = VerifySignature(
                dto.RazorpayOrderId,
                dto.RazorpayPaymentId,
                dto.RazorpaySignature,
                keySecret);

            if (!isValid)
                return (false, "Payment verification failed.");

            // Step 2 — Save to Payment table
            var payment = new EInsurance.Models.Entities.Payment
            {
                CustomerID = dto.CustomerID,
                PolicyID = dto.PolicyID,
                Amount = dto.Amount,
                PaymentDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return (true,
                "Payment of ₹" + dto.Amount.ToString("N2") +
                " successful!");
        }

        // Verify Razorpay Signature 
        // Razorpay signs: orderId + paymentId
        // using HMAC SHA256 with your key secret
        private bool VerifySignature(
            string orderId, string paymentId,
            string signature, string secret)
        {
            var payload = orderId + "|" + paymentId;
            var key = Encoding.UTF8.GetBytes(secret);
            var message = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(message);
            var expectedSignature = BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();

            return expectedSignature == signature;
        }
    }
}
