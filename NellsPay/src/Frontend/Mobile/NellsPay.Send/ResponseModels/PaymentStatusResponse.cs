using System;
namespace NellsPay.Send.ResponseModels
{
    public class PaymentStatusResponse
    {
        public List<PaymentStatus> paymentStatuses { get; set; }
    }
    
    public class PaymentStatus
    {
        public string paymentId { get; set; }
        public string customerId { get; set; }
        public string status { get; set; }
        public object rawProviderStatus { get; set; }
        public DateTime receivedAt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModified { get; set; }
    }
}
