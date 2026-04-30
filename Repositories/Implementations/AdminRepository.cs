using EInsurance.Data;
using EInsurance.Models.DTOs;
using EInsurance.Models.Entities;
using EInsurance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        //Dashboard Stats
        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            return new DashboardStatsDto
            {
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalAgents = await _context.InsuranceAgents.CountAsync(),
                TotalEmployees = await _context.Employees.CountAsync(),
                TotalPolicies = await _context.Policies.CountAsync(),
                TotalPayments = await _context.Payments.CountAsync(),
                TotalRevenue = await _context.Payments.SumAsync(p => p.Amount)
            };
        }

        // Customers
        public async Task<PagedResultDto<CustomerListDto>> GetAllCustomersAsync(string search, int page, int pageSize)
        {
            var query = GetCustomerQuery(search);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<CustomerListDto>
            {
                Items = items,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Search = search
            };
        }

        private IQueryable<CustomerListDto> GetCustomerQuery(string search)
        {
            var query = _context.Customers
                .Include(c => c.Agent)
                .Include(c => c.Policies)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(c =>
                    c.FullName.ToLower().Contains(search) ||
                    c.Email.ToLower().Contains(search) ||
                    c.Phone.Contains(search));
            }

            return query.Select(c => new CustomerListDto
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                DateOfBirth = c.DateOfBirth,
                AgentName = c.Agent != null ? c.Agent.FullName : "N/A",
                TotalPolicies = c.Policies.Count,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
            => await _context.Customers
                .Include(c => c.Agent)
                .FirstOrDefaultAsync(c => c.CustomerID == id);

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;
            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        // Agents
        public async Task<PagedResultDto<AgentListDto>> GetAllAgentsAsync(string search, int page, int pageSize)
        {
            var query = GetAgentQuery(search);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<AgentListDto>
            {
                Items = items,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Search = search
            };
        }

        private IQueryable<AgentListDto> GetAgentQuery(string search)
        {
            var query = _context.InsuranceAgents
                .Include(a => a.Customers)
                .Include(a => a.Commissions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(a =>
                    a.FullName.ToLower().Contains(search) ||
                    a.Email.ToLower().Contains(search) ||
                    a.Username.ToLower().Contains(search));
            }

            return query.Select(a => new AgentListDto
            {
                AgentID = a.AgentID,
                Username = a.Username,
                FullName = a.FullName,
                Email = a.Email,
                TotalCustomers = a.Customers.Count,
                TotalCommissions = a.Commissions.Count,
                CreatedAt = a.CreatedAt
            });
        }

        public async Task<InsuranceAgent?> GetAgentByIdAsync(int id)
            => await _context.InsuranceAgents
                .FirstOrDefaultAsync(a => a.AgentID == id);

        public async Task<bool> UpdateAgentAsync(InsuranceAgent agent)
        {
            _context.InsuranceAgents.Update(agent);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAgentAsync(int id)
        {
            var agent = await _context.InsuranceAgents.FindAsync(id);
            if (agent == null) return false;
            _context.InsuranceAgents.Remove(agent);
            return await _context.SaveChangesAsync() > 0;
        }

        // Employees 
        public async Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(string search, int page, int pageSize)
        {
            var query = GetEmployeeQuery(search);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<EmployeeListDto>
            {
                Items = items,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Search = search
            };
        }

        private IQueryable<EmployeeListDto> GetEmployeeQuery(string search)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(e =>
                    e.FullName.ToLower().Contains(search) ||
                    e.Email.ToLower().Contains(search) ||
                    e.Username.ToLower().Contains(search) ||
                    e.Role.ToLower().Contains(search));
            }

            return query.Select(e => new EmployeeListDto
            {
                EmployeeID = e.EmployeeID,
                Username = e.Username,
                FullName = e.FullName,
                Email = e.Email,
                Role = e.Role,
                CreatedAt = e.CreatedAt
            });
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
            => await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
