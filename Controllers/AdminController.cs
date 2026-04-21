using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EInsurance.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;

        public AdminController(IAdminService adminService, IAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
        }

        // Auth Check 
        private bool IsAdmin()
            => HttpContext.Session.GetString("Role") == "Admin";

        private IActionResult RedirectIfNotAdmin()
            => RedirectToAction("Login", "Auth");


        // DASHBOARD

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var stats = await _adminService.GetDashboardStatsAsync();
            return View(stats);
        }


        // CUSTOMERS

        public async Task<IActionResult> Customers(string search = "")
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var customers = string.IsNullOrEmpty(search)
                ? await _adminService.GetAllCustomersAsync()
                : await _adminService.SearchCustomersAsync(search);
            ViewBag.Search = search;
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var customer = await _adminService.GetCustomerForEditAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(EditCustomerDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var (success, message) = await _adminService.UpdateCustomerAsync(dto);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Customers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var (success, message) = await _adminService.DeleteCustomerAsync(id);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Customers");
        }


        // AGENTS

        public async Task<IActionResult> Agents(string search = "")
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var agents = string.IsNullOrEmpty(search)
                ? await _adminService.GetAllAgentsAsync()
                : await _adminService.SearchAgentsAsync(search);
            ViewBag.Search = search;
            return View(agents);
        }

        [HttpGet]
        public async Task<IActionResult> EditAgent(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var agent = await _adminService.GetAgentForEditAsync(id);
            if (agent == null) return NotFound();
            return View(agent);
        }

        [HttpPost]
        public async Task<IActionResult> EditAgent(EditAgentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var (success, message) = await _adminService.UpdateAgentAsync(dto);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Agents");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var (success, message) = await _adminService.DeleteAgentAsync(id);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Agents");
        }

        [HttpGet]
        public IActionResult RegisterAgent()
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAgent(CreateAgentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var (success, message) = await _authService.CreateAgentAsync(dto);
            TempData[success ? "Success" : "Error"] = message;
            if (!success) return View(dto);
            return RedirectToAction("Agents");
        }


        // EMPLOYEES

        public async Task<IActionResult> Employees(string search = "")
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var employees = string.IsNullOrEmpty(search)
                ? await _adminService.GetAllEmployeesAsync()
                : await _adminService.SearchEmployeesAsync(search);
            ViewBag.Search = search;
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var employee = await _adminService.GetEmployeeForEditAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var (success, message) = await _adminService.UpdateEmployeeAsync(dto);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Employees");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            var (success, message) = await _adminService.DeleteEmployeeAsync(id);
            TempData[success ? "Success" : "Error"] = message;
            return RedirectToAction("Employees");
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            if (!IsAdmin()) return RedirectIfNotAdmin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var (success, message) = await _authService.CreateEmployeeAsync(dto);
            TempData[success ? "Success" : "Error"] = message;
            if (!success) return View(dto);
            return RedirectToAction("Employees");
        }
    }
}
