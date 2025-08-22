using System;
namespace NellsPay.Send.ResponseModels
{
   

    public class PaymentWrapper : ApiErrorResponse
    {
        public PaymentResponse paymentResponse { get; set; }
    }

    public class PaymentResponse 
    {
        public string paymentUrl { get; set; }
        public string customerId { get; set; }
        public string paymentId { get; set; }
        public DateTime expiresAt { get; set; }
    }
}
