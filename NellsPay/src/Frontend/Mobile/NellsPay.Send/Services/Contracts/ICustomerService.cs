using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerResponse?> GetCustomerByEmail(string email);
        Task<CustomerResponse?> GetCustomerById(string id);
        Task<SaltbyemailResponse> GetSaltbyemail(string email);
        Task<CustomerIdResponse?> AddSECustomer(AddSeCustomerWrapper addSeCustomer, string customerId);
        Task<CreateTransactionResponse> CreateTransaction(CreateTransactionWrapper createTransactionWrapper);
        Task<GetTransactionResponse> GetTransaction(int PageIndex, int PageSize);
        Task<EditCustomerResponse> EditCustomer(EditCustomerWrapper editCustomerWrapper);
    }
}

