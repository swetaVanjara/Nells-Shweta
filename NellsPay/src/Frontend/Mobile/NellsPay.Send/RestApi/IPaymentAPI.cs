using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface IPaymentAPI
    {
        [Get("/payments/providers/{countryCode}")]
        Task<PaymentsProvidersResponse?> GetPaymentProvider([Header("Authorization")] string authorization, string countryCode, [Query] int PageIndex, [Query] int PageSize);

        [Get("/payments/{paymentId}/status")]
        Task<PaymentStatusResponse?> GetPaymentStatus([Header("Authorization")] string authorization, string paymentId);

        [Post("/payments/payment-initiation")]
        Task<PaymentWrapper> CreatePayment([Header("Authorization")] string authorization, [Body] PaymentInitiationWrapper paymentInitiationWrapper);
    }
}