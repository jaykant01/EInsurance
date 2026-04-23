using EInsurance.Models.DTOs;

namespace EInsurance.Repositories.Interfaces
{
    public interface IReceiptRepository
    {
        // Get all payments for a customer
        Task<List<CustomerPaymentListDto>> GetCustomerPaymentsAsync(int customerId);

        // Get single payment details for receipt
        Task<PaymentReceiptDto?> GetPaymentReceiptAsync(int paymentId, int customerId);
    }
}
