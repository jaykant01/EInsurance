using EInsurance.Models.DTOs;
using EInsurance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EInsurance.Controllers
{
    public class AuthViewController : Controller
    {
        private readonly IAuthService _authService;

        public AuthViewController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET /AuthView/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST /AuthView/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                ViewBag.Error = "Invalid credentials or role.";
                return View(dto);
            }

            HttpContext.Session.SetString("Token", result.Token);
            HttpContext.Session.SetString("Role", result.Role);
            HttpContext.Session.SetString("FullName", result.FullName);
            HttpContext.Session.SetString("UserId", result.UserId.ToString());

            return result.Role switch
            {
                "Admin" => RedirectToAction("Dashboard", "Admin"),
                "Employee" => RedirectToAction("Dashboard", "Employee"),
                "Agent" => RedirectToAction("Dashboard", "Agent"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // GET /AuthView/CustomerLogin
        [HttpGet]
        public IActionResult CustomerLogin()
        {
            return View();
        }

        // POST /AuthView/CustomerLogin
        [HttpPost]
        public async Task<IActionResult> CustomerLogin(CustomerLoginRequestDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _authService.CustomerLoginAsync(dto);
            if (result == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View(dto);
            }

            HttpContext.Session.SetString("Token", result.Token);
            HttpContext.Session.SetString("Role", result.Role);
            HttpContext.Session.SetString("FullName", result.FullName);
            HttpContext.Session.SetString("UserId", result.UserId.ToString());

            return RedirectToAction("MyPolicies", "Policy");
        }

        // GET /AuthView/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST /AuthView/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCustomerDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var (success, message) = await _authService.RegisterCustomerAsync(dto);
            if (!success)
            {
                ViewBag.Error = message;
                return View(dto);
            }

            ViewBag.Success = "Registered successfully! Please login.";
            return View();
        }

        // GET /AuthView/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
