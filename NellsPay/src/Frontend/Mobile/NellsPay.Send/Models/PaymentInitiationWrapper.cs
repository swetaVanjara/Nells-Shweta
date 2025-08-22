using CommunityToolkit.Maui.Core;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Models
{
    public class PaymentInitiationWrapper
    {
        public PaymentInitiationRequest paymentInitiationRequest { get; set; }
    }
    public class PaymentInitiationRequest
    {
        public string? deviceOs { get; set; }
        public string? deviceIpAddress { get; set; }
        public string? transactionNumber { get; set; }
        public string? amount { get; set; }
        public string? providerCode { get; set; }
        public string? templateIdentifier { get; set; }
        public string? customerId { get; set; }
        public string? countryCode { get; set; }
    }
}