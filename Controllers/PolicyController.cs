using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EInsurance.Controllers
{
    public class PolicyController : Controller
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // Helper — check session 
        private bool IsLoggedIn() =>
            HttpContext.Session.GetString("Role") != null;

        private int GetUserId() =>
            int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

        private string GetRole() =>
            HttpContext.Session.GetString("Role") ?? "";

        public async Task<IActionResult> MyPolicies()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "AuthView");

            if (GetRole() != "Customer")
                return RedirectToAction("Index", "Home");

            var customerId = GetUserId();
            var policies = await _policyService.GetMyPoliciesAsync(customerId);

            return View(policies);
        }

        public async Task<IActionResult> CustomerPolicies(string search = "")
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "AuthView");

            if (GetRole() != "Admin")
                return RedirectToAction("Index", "Home");

            var customers = await _policyService.SearchCustomersAsync(search);

            ViewBag.Search = search;
            return View(customers);
        }

        public async Task<IActionResult> CustomerPolicyDetails(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "AuthView");

            if (GetRole() != "Admin")
                return RedirectToAction("Index", "Home");

            var policies = await _policyService.GetCustomerPoliciesAsync(id);

            // Pass customer name to view
            ViewBag.CustomerName = policies.FirstOrDefault()?.CustomerName ?? "Customer";
            ViewBag.CustomerId = id;

            return View(policies);
        }
    }
}
