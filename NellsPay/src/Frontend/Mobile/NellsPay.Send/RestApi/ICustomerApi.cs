using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface ICustomerApi
    {
        [Get("/customers/{id}")]
        Task<CustomerResponse?> GetCustomerById([Header("Authorization")] string authorization, string id);

        [Get("/customers/{email}")]
        Task<CustomerResponse?> GetCustomerByEmail([Header("Authorization")] string authorization, string email);

        [Get("/customers/{customerId}/recipients")]
        Task<RecipientResponse> GetRecipientsByCustomerId([Header("Authorization")] string authorization, string customerId);

        [Get("/customers/se/by-email")]
        Task<SaltbyemailResponse> GetSaltbyemail([Header("Authorization")] string authorization, string email);

        [Put("/customers")]
        Task<EditCustomerResponse> EditCustomer([Header("Authorization")] string authorization, [Body] EditCustomerWrapper editCustomerWrapper);

        [Post("/customers/{customerId}/recipients")]
        Task<AddRecipientResponse?> AddRecipientsByCustomerId([Header("Authorization")] string authorization, string customerId, [Body] RecipientWrapper recipient);

        [Put("/customers/{customerId}/recipients")]
        Task<EditRecipientResponse?> EditRecipientsByCustomerId([Header("Authorization")] string authorization, string customerId, [Body] RecipientWrapper recipient);

        // [Post("/customers/payment-pis")]
        // Task<AddRecipientResponse?> AddSECustomer([Header("Authorization")] string authorization, [Body] AddSeCustomerWrapper addSeCustomer);
        [Post("/customers/{customerId}/payment-pis")]
        Task<CustomerIdResponse?> AddSECustomer([Header("Authorization")] string authorization, [Body] AddSeCustomerWrapper addSeCustomer, string customerId);

        [Post("/transactions")]
        Task<CreateTransactionResponse> CreateTransaction([Header("Authorization")] string authorization, [Body] CreateTransactionWrapper createTransactionWrapper);

        [Get("/transactions")]
        Task<GetTransactionResponse> GetTransaction([Header("Authorization")] string authorization, [Query] int PageIndex, [Query] int PageSize);
        
        // [Post("/customers/{customerId}/recipients")]
        // Task<EditRecipientResponse?> EditRecipientsByCustomerId([Header("Authorization")] string authorization, string customerId, [Body] Recipient recipient);
    }
}