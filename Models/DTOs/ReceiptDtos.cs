namespace EInsurance.Models.DTOs
{
    public class PaymentReceiptDto
    {
        // Receipt Info
        public int PaymentID { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Customer Info
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        // Policy Info
        public int PolicyID { get; set; }
        public string PolicyDetails { get; set; } = string.Empty;
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal Premium { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime PolicyLapseDate { get; set; }

        // Agent Info
        public string AgentName { get; set; } = string.Empty;
    }

    public class CustomerPaymentListDto
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PolicyID { get; set; }
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string ReceiptNumber { get; set; } = string.Empty;
    }
}
