using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface ITransactionAPI
    {
        [Get("/transactions")]
        Task<TransactionResponse?> GetTransactions([Header("Authorization")] string authorization, [Query] int PageIndex, [Query] int PageSize);
        
        [Get("/transactions/customer/{customerId}/summary")]
        Task<TransactionSummaryResponse?> GetTransactionsSummary([Header("Authorization")] string authorization, string customerId);
    }
}