using System.ComponentModel.DataAnnotations;

namespace EInsurance.Models.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalCustomers { get; set; }
        public int TotalAgents { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalPolicies { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class CustomerListDto
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public int TotalPolicies { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EditCustomerDto
    {
        public int CustomerID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int AgentID { get; set; }
    }

    public class AgentListDto
    {
        public int AgentID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalCustomers { get; set; }
        public int TotalCommissions { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EditAgentDto
    {
        public int AgentID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class EmployeeListDto
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class EditEmployeeDto
    {
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }
}
