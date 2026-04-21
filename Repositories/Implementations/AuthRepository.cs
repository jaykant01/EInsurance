using EInsurance.Data;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgentEmailExistsAsync(string email)
        {
            return await _context.InsuranceAgents
                .AnyAsync(a => a.Email == email);
        }

        public async Task<bool> AgentUsernameExistsAsync(string username)
        {
            return await _context.InsuranceAgents
                .AnyAsync(a => a.Username == username);
        }

        public async Task<InsuranceAgent> CreateAgentAsync(InsuranceAgent agent)
        {
            _context.InsuranceAgents.Add(agent);
            await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> CustomerEmailExistsAsync(string email)
        {
            return await _context.Customer
                .AnyAsync(c => c.Email == email);
        }

        public async Task<bool> EmployeeEmailExistsAsync(string email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email);
        }

        public async Task<bool> EmployeeUsernameExistsAsync(string username)
        {
            return await _context.Employees
               .AnyAsync(e => e.Username == username);
        }

        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<InsuranceAgent?> GetAgentByUsernameAsync(string username)
        {
            return await _context.InsuranceAgents
                .FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Employee?> GetEmployeeByUsernameAsync(string username)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Username == username);
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
