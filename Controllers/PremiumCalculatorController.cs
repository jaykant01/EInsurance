using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EInsurance.Controllers
{
    public class PremiumCalculatorController : Controller
    {
        private readonly IPremiumCalculatorService _calculatorService;

        public PremiumCalculatorController(
            IPremiumCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        // Auth Check 
        private bool IsAuthenticated()
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "Customer" || role == "Agent";
        }

        //  Load Scheme Dropdown 
        private async Task LoadSchemeDropdownAsync()
        {
            var schemes = await _calculatorService.GetSchemesForDropdownAsync();
            ViewBag.Schemes = new SelectList(schemes, "SchemeID", "DisplayName");
        }


        // GET /PremiumCalculator/Calculate

        [HttpGet]
        public async Task<IActionResult> Calculate()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Auth");

            await LoadSchemeDropdownAsync();

            // Pre-fill with defaults
            var model = new PremiumCalculatorInputDto
            {
                Age = 30,
                MaturityPeriod = 10,
                SumAssured = 100000,
                RateOfInterest = 5
            };

            return View(model);
        }


        // POST /PremiumCalculator/Calculate

        [HttpPost]
        public async Task<IActionResult> Calculate(PremiumCalculatorInputDto input)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Auth");

            await LoadSchemeDropdownAsync();

            if (!ModelState.IsValid)
                return View(input);

            // Get scheme name for display
            var schemes = await _calculatorService.GetSchemesForDropdownAsync();
            var selected = schemes.FirstOrDefault(s => s.SchemeID == input.SchemeID);
            if (selected != null)
            {
                var parts = selected.DisplayName.Split(" → ");
                input.PlanName = parts.Length > 0 ? parts[0] : "";
                input.SchemeName = parts.Length > 1 ? parts[1] : "";
            }

            // Calculate
            var result = _calculatorService.Calculate(input);

            // Pass both input and result to view
            ViewBag.Result = result;
            return View(input);
        }
    }
}
