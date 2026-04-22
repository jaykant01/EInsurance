namespace EInsurance.Models.DTOs
{
    // Summary per agent shown in list
    public class AgentCommissionSummaryDto
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalPoliciesSold { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public DateTime? LastCommissionDate { get; set; }
    }

    // Detailed commission for one agent
    public class AgentCommissionDetailDto
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal CommissionRate { get; set; }
        public int TotalPoliciesSold { get; set; }
        public decimal TotalPremiumGenerated { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public List<PolicyCommissionDto> PolicyCommissions { get; set; } = new();
    }

    // Commission per policy
    public class PolicyCommissionDto
    {
        public int PolicyID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal Premium { get; set; }
        public decimal CommissionAmount { get; set; }
        public DateTime DateIssued { get; set; }
    }

    // Input for calculating commission
    public class CommissionCalculateDto
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; } = string.Empty;

        // Admin sets commission rate
        public decimal CommissionRate { get; set; } = 10; // default 10%
    }
}
