using EInsurance.Data;
using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static EInsurance.Models.DTOs.PolicyPurchaseDtos;

namespace EInsurance.Controllers
{
    public class PolicyPurchaseController : Controller
    {
        private readonly IPolicyPurchaseService _purchaseService;
        private readonly AppDbContext _context;

        public PolicyPurchaseController(
            IPolicyPurchaseService purchaseService,
            AppDbContext context)
        {
            _purchaseService = purchaseService;
            _context = context;
        }

        // Helpers
        private bool IsCustomer()
            => HttpContext.Session.GetString("Role") == "Customer";

        private int GetCustomerId()
            => int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

        private async Task<int> GetCustomerAgeAsync(int customerId)
        {
            var customer = await _context.Customers
                .FindAsync(customerId);
            if (customer == null) return 30;
            var today = DateTime.Today;
            var age = today.Year - customer.DateOfBirth.Year;
            if (customer.DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        // STEP 1 — Browse Plans
        public async Task<IActionResult> AvailablePlans()
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Auth");

            var plans = await _purchaseService.GetAllPlansAsync();
            return View(plans);
        }

        // STEP 2 — Browse Schemes
        public async Task<IActionResult> AvailableSchemes(int planId = 0)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Auth");

            var schemes = planId == 0
                ? await _purchaseService.GetAllSchemesAsync()
                : await _purchaseService.GetSchemesByPlanIdAsync(planId);

            ViewBag.PlanId = planId;
            return View(schemes);
        }

        // STEP 3 — Purchase Form
        [HttpGet]
        public async Task<IActionResult> Purchase(int schemeId)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Auth");

            var customerId = GetCustomerId();
            var age = await GetCustomerAgeAsync(customerId);
            var form = await _purchaseService.GetPurchaseFormAsync(schemeId, age);

            if (form == null) return NotFound();

            ViewBag.CustomerAge = age;
            return View(form);
        }

        // STEP 3 — Purchase Submit
        [HttpPost]
        public async Task<IActionResult> Purchase(PurchasePolicyDto dto)
        {
            if (!IsCustomer())
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(dto);

            var customerId = GetCustomerId();
            var (success, message) = await _purchaseService
                .PurchasePolicyAsync(dto, customerId);

            if (!success)
            {
                ViewBag.Error = message;
                var age = await GetCustomerAgeAsync(customerId);
                ViewBag.CustomerAge = age;
                return View(dto);
            }

            TempData["Success"] = message;
            return RedirectToAction("MyPolicies", "Policy");
        }
    }
}
