using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IPaymentService
    {
        // Create Razorpay order
        Task<RazorpayOrderDto?> CreateOrderAsync(
            int policyId, int customerId);

        // Verify payment and save to DB
        Task<(bool Success, string Message)> VerifyAndSavePaymentAsync(
            PaymentVerificationDto dto);
    }
}
