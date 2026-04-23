using EInsurance.Helpers;
using EInsurance.Models.DTOs;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;

namespace EInsurance.Services.Implementations
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepo;
        private readonly PdfReceiptHelper _pdfHelper;

        public ReceiptService(
            IReceiptRepository receiptRepo,
            PdfReceiptHelper pdfHelper)
        {
            _receiptRepo = receiptRepo;
            _pdfHelper = pdfHelper;
        }

        public async Task<List<CustomerPaymentListDto>> GetCustomerPaymentsAsync(
            int customerId)
            => await _receiptRepo.GetCustomerPaymentsAsync(customerId);

        public async Task<PaymentReceiptDto?> GetPaymentReceiptAsync(
            int paymentId, int customerId)
            => await _receiptRepo.GetPaymentReceiptAsync(paymentId, customerId);

        public async Task<byte[]?> GenerateReceiptPdfAsync(
            int paymentId, int customerId)
        {
            var receipt = await _receiptRepo
                .GetPaymentReceiptAsync(paymentId, customerId);

            if (receipt == null) return null;

            return _pdfHelper.GenerateReceipt(receipt);
        }
    }
}
