using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EInsurance.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        //  Auth Check
        private bool IsCustomer()
            => HttpContext.Session.GetString("Role") == "Customer";

        private int GetCustomerId()
            => int.Parse(
                HttpContext.Session.GetString("UserId") ?? "0");


        // GET /Receipt/MyPayments
        // Shows all payments with download button
        public async Task<IActionResult> MyPayments()
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "AuthView");

            var customerId = GetCustomerId();
            var payments = await _receiptService
                .GetCustomerPaymentsAsync(customerId);

            return View(payments);
        }

        // Shows receipt in browser before download
        public async Task<IActionResult> ViewReceipt(int id)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "AuthView");

            var customerId = GetCustomerId();
            var receipt = await _receiptService
                .GetPaymentReceiptAsync(id, customerId);

            if (receipt == null)
                return NotFound();

            return View(receipt);
        }

        // Downloads PDF receipt
        public async Task<IActionResult> Download(int id)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "AuthView");

            var customerId = GetCustomerId();
            var pdfBytes = await _receiptService
                .GenerateReceiptPdfAsync(id, customerId);

            if (pdfBytes == null)
                return NotFound();

            var fileName = $"Receipt-RCPT-{id:D6}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
