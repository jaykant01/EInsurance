using EInsurance.Helpers;
using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;

namespace EInsurance.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepo, JwtHelper jwtHelper)
        {
            _authRepo = authRepo;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            switch (request.Role)
            {
                case "Admin":
                    var admin = await _authRepo.GetAdminByUsernameAsync(request.Username);
                    if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
                        return null;
                    return new LoginResponseDto
                    {
                        Token = _jwtHelper.GenerateToken(admin.AdminID, admin.Username, admin.Email, "Admin"),
                        Role = "Admin",
                        FullName = admin.FullName,
                        Email = admin.Email,
                        UserId = admin.AdminID
                    };

                case "Employee":
                    var employee = await _authRepo.GetEmployeeByUsernameAsync(request.Username);
                    if (employee == null || !BCrypt.Net.BCrypt.Verify(request.Password, employee.Password))
                        return null;
                    return new LoginResponseDto
                    {
                        Token = _jwtHelper.GenerateToken(employee.EmployeeID, employee.Username, employee.Email, "Employee"),
                        Role = "Employee",
                        FullName = employee.FullName,
                        Email = employee.Email,
                        UserId = employee.EmployeeID
                    };

                case "Agent":
                    var agent = await _authRepo.GetAgentByUsernameAsync(request.Username);
                    if (agent == null || !BCrypt.Net.BCrypt.Verify(request.Password, agent.Password))
                        return null;
                    return new LoginResponseDto
                    {
                        Token = _jwtHelper.GenerateToken(agent.AgentID, agent.Username, agent.Email, "Agent"),
                        Role = "Agent",
                        FullName = agent.FullName,
                        Email = agent.Email,
                        UserId = agent.AgentID
                    };

                default:
                    return null;
            }
        }

        public async Task<LoginResponseDto?> CustomerLoginAsync(CustomerLoginRequestDto request)
        {
            var customer = await _authRepo.GetCustomerByEmailAsync(request.Email);
            if (customer == null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.Password))
                return null;

            return new LoginResponseDto
            {
                Token = _jwtHelper.GenerateToken(
                    customer.CustomerID, customer.Email, customer.Email, "Customer"),
                Role = "Customer",
                FullName = customer.FullName,
                Email = customer.Email,
                UserId = customer.CustomerID
            };
        }

        public async Task<(bool Success, string Message)> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            if (await _authRepo.CustomerEmailExistsAsync(dto.Email))
                return (false, "Email already registered.");

            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                AgentID = dto.AgentID,
                CreatedAt = DateTime.UtcNow
            };

            await _authRepo.RegisterCustomerAsync(customer);
            return (true, "Customer registered successfully.");
        }

        public async Task<(bool Success, string Message)> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            if (await _authRepo.EmployeeUsernameExistsAsync(dto.Username))
                return (false, "Username already taken.");

            if (await _authRepo.EmployeeEmailExistsAsync(dto.Email))
                return (false, "Email already registered.");

            var employee = new Employee
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _authRepo.CreateEmployeeAsync(employee);
            return (true, "Employee created successfully.");
        }

        public async Task<(bool Success, string Message)> CreateAgentAsync(CreateAgentDto dto)
        {
            if (await _authRepo.AgentUsernameExistsAsync(dto.Username))
                return (false, "Username already taken.");

            if (await _authRepo.AgentEmailExistsAsync(dto.Email))
                return (false, "Email already registered.");

            var agent = new InsuranceAgent
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                CreatedAt = DateTime.UtcNow
            };

            await _authRepo.CreateAgentAsync(agent);
            return (true, "Agent created successfully.");
        }
    }
}
