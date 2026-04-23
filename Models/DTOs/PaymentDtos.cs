namespace EInsurance.Models.DTOs
{
    public class InitiatePaymentDto
    {
        public int PolicyID { get; set; }
        public decimal Amount { get; set; }
        public string SchemeName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
    }

    public class RazorpayOrderDto
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string KeyId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string PolicyDescription { get; set; } = string.Empty;
        public int PolicyID { get; set; }
    }

    public class PaymentVerificationDto
    {
        public string RazorpayOrderId { get; set; } = string.Empty;
        public string RazorpayPaymentId { get; set; } = string.Empty;
        public string RazorpaySignature { get; set; } = string.Empty;
        public int PolicyID { get; set; }
        public int CustomerID { get; set; }
        public decimal Amount { get; set; }
    }
}
