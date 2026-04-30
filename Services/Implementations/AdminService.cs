using EInsurance.Models.DTOs;
using EInsurance.Repositories.Interfaces;
using EInsurance.Services.Interfaces;

namespace EInsurance.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepo;

        public AdminService(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        // Dashboard 
        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
            => await _adminRepo.GetDashboardStatsAsync();

        // Customers 
        public async Task<PagedResultDto<CustomerListDto>> GetAllCustomersAsync(string search, int page, int pageSize)
            => await _adminRepo.GetAllCustomersAsync(search, page, pageSize);


        public async Task<EditCustomerDto?> GetCustomerForEditAsync(int id)
        {
            var c = await _adminRepo.GetCustomerByIdAsync(id);
            if (c == null) return null;
            return new EditCustomerDto
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                DateOfBirth = c.DateOfBirth,
                AgentID = c.AgentID
            };
        }

        public async Task<(bool Success, string Message)> UpdateCustomerAsync(EditCustomerDto dto)
        {
            var c = await _adminRepo.GetCustomerByIdAsync(dto.CustomerID);
            if (c == null) return (false, "Customer not found.");
            c.FullName = dto.FullName;
            c.Email = dto.Email;
            c.Phone = dto.Phone;
            c.DateOfBirth = dto.DateOfBirth;
            c.AgentID = dto.AgentID;
            var result = await _adminRepo.UpdateCustomerAsync(c);
            return result ? (true, "Customer updated successfully.") : (false, "Update failed.");
        }

        public async Task<(bool Success, string Message)> DeleteCustomerAsync(int id)
        {
            var result = await _adminRepo.DeleteCustomerAsync(id);
            return result ? (true, "Customer deleted successfully.") : (false, "Delete failed.");
        }

        //  Agents
        public async Task<PagedResultDto<AgentListDto>> GetAllAgentsAsync(string search, int page, int pageSize)
            => await _adminRepo.GetAllAgentsAsync(search, page, pageSize);

        public async Task<EditAgentDto?> GetAgentForEditAsync(int id)
        {
            var a = await _adminRepo.GetAgentByIdAsync(id);
            if (a == null) return null;
            return new EditAgentDto
            {
                AgentID = a.AgentID,
                Username = a.Username,
                FullName = a.FullName,
                Email = a.Email
            };
        }

        public async Task<(bool Success, string Message)> UpdateAgentAsync(EditAgentDto dto)
        {
            var a = await _adminRepo.GetAgentByIdAsync(dto.AgentID);
            if (a == null) return (false, "Agent not found.");
            a.Username = dto.Username;
            a.FullName = dto.FullName;
            a.Email = dto.Email;
            var result = await _adminRepo.UpdateAgentAsync(a);
            return result ? (true, "Agent updated successfully.") : (false, "Update failed.");
        }

        public async Task<(bool Success, string Message)> DeleteAgentAsync(int id)
        {
            var result = await _adminRepo.DeleteAgentAsync(id);
            return result ? (true, "Agent deleted successfully.") : (false, "Delete failed.");
        }

        // Employees
        public async Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(string search, int page, int pageSize)
           => await _adminRepo.GetAllEmployeesAsync(search, page, pageSize);

        public async Task<EditEmployeeDto?> GetEmployeeForEditAsync(int id)
        {
            var e = await _adminRepo.GetEmployeeByIdAsync(id);
            if (e == null) return null;
            return new EditEmployeeDto
            {
                EmployeeID = e.EmployeeID,
                Username = e.Username,
                FullName = e.FullName,
                Email = e.Email,
                Role = e.Role
            };
        }

        public async Task<(bool Success, string Message)> UpdateEmployeeAsync(EditEmployeeDto dto)
        {
            var e = await _adminRepo.GetEmployeeByIdAsync(dto.EmployeeID);
            if (e == null) return (false, "Employee not found.");
            e.Username = dto.Username;
            e.FullName = dto.FullName;
            e.Email = dto.Email;
            e.Role = dto.Role;
            var result = await _adminRepo.UpdateEmployeeAsync(e);
            return result ? (true, "Employee updated successfully.") : (false, "Update failed.");
        }

        public async Task<(bool Success, string Message)> DeleteEmployeeAsync(int id)
        {
            var result = await _adminRepo.DeleteEmployeeAsync(id);
            return result ? (true, "Employee deleted successfully.") : (false, "Delete failed.");
        }
    }
}
