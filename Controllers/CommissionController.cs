using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EInsurance.Controllers
{
    public class CommissionController : Controller
    {
        private readonly ICommissionService _commissionService;
        private readonly IAdminService _adminService;

        public CommissionController(
            ICommissionService commissionService,
            IAdminService adminService)
        {
            _commissionService = commissionService;
            _adminService = adminService;
        }

        //  Auth Check 
        private bool IsAdmin()
            => HttpContext.Session.GetString("Role") == "Admin";

        //  Load Agent Dropdown
        private async Task LoadAgentDropdownAsync()
        {
            var agents = await _adminService.GetAllAgentsAsync();
            ViewBag.Agents = new SelectList(
                agents, "AgentID", "FullName");
        }

        // Shows all agents with commission summary

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Auth");

            var summary = await _commissionService
                .GetAllAgentCommissionSummaryAsync();

            return View(summary);
        }

        // GET /Commission/Calculate
        // Admin selects agent + rate to calculate

        [HttpGet]
        public async Task<IActionResult> Calculate(
            int agentId = 0, decimal commissionRate = 10)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Auth");

            await LoadAgentDropdownAsync();

            ViewBag.SelectedAgentId = agentId;
            ViewBag.CommissionRate = commissionRate;
            ViewBag.Detail = null;

            // If agent selected → calculate
            if (agentId > 0)
            {
                var detail = await _commissionService
                    .CalculateAgentCommissionAsync(agentId, commissionRate);
                ViewBag.Detail = detail;
            }

            return View();
        }

        // POST /Commission/Save
        // Admin saves commission to DB

        [HttpPost]
        public async Task<IActionResult> Save(
            int agentId, decimal commissionRate)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Auth");

            var (success, message) = await _commissionService
                .SaveCommissionsAsync(agentId, commissionRate);

            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Index");
        }
    }
}
