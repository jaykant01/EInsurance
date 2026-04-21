using EInsurance.Models.DTOs;

namespace EInsurance.Services.Interfaces
{
    public interface IAuthService
    {
        // Login
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
        Task<LoginResponseDto?> CustomerLoginAsync(CustomerLoginRequestDto request);

        // Registration
        Task<(bool Success, string Message)> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<(bool Success, string Message)> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<(bool Success, string Message)> CreateAgentAsync(CreateAgentDto dto);
    }
}
