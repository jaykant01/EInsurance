namespace EInsurance.Models.DTOs
{
    // Customer views their own policy
    public class CustomerPolicyDto
    {
        public int PolicyID { get; set; }
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string PolicyDetails { get; set; } = string.Empty;
        public decimal Premium { get; set; }
        public DateTime DateIssued { get; set; }
        public int MaturityPeriod { get; set; }
        public DateTime PolicyLapseDate { get; set; }
        public List<PaymentDetailDto> Payments { get; set; } = new();
    }

    // Payment detail inside policy
    public class PaymentDetailDto
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    // Admin views customer specific policies
    public class AdminCustomerPolicyDto
    {
        public int PolicyID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string PolicyDetails { get; set; } = string.Empty;
        public decimal Premium { get; set; }
        public DateTime DateIssued { get; set; }
        public int MaturityPeriod { get; set; }
        public DateTime PolicyLapseDate { get; set; }
        public decimal TotalPaid { get; set; }
        public List<PaymentDetailDto> Payments { get; set; } = new();
    }

    // Admin searches customer
    public class CustomerSearchDto
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AgentName { get; set; } = string.Empty;
        public int TotalPolicies { get; set; }
    }
}
