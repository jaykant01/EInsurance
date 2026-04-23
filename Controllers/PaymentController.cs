using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EInsurance.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Auth Check 
        private bool IsCustomer()
            => HttpContext.Session.GetString("Role") == "Customer";

        private int GetCustomerId()
            => int.Parse(
                HttpContext.Session.GetString("UserId") ?? "0");


        // Shows Razorpay payment page for a policy
        public async Task<IActionResult> Pay(int policyId)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "AuthView");

            var customerId = GetCustomerId();
            var order = await _paymentService
                .CreateOrderAsync(policyId, customerId);

            if (order == null)
                return NotFound();

            // Pass customer ID for verification later
            ViewBag.CustomerId = customerId;

            return View(order);
        }

        // POST /Payment/Verify
        // Called after Razorpay payment succeeds

        [HttpPost]
        public async Task<IActionResult> Verify(
            PaymentVerificationDto dto)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "AuthView");

            dto.CustomerID = GetCustomerId();

            var (success, message) = await _paymentService
                .VerifyAndSavePaymentAsync(dto);

            if (!success)
            {
                TempData["Error"] = message;
                return RedirectToAction("MyPolicies", "Policy");
            }

            TempData["Success"] = message;
            return RedirectToAction("MyPayments", "Receipt");
        }
    }
}
