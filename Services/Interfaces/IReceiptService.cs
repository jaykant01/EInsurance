using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<List<CustomerPaymentListDto>> GetCustomerPaymentsAsync(int customerId);
        Task<PaymentReceiptDto?> GetPaymentReceiptAsync(int paymentId, int customerId);
        Task<byte[]?> GenerateReceiptPdfAsync(int paymentId, int customerId);
    }
}
