using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.Services.Contracts
{
    public interface ITransactionService
    {
        // Task<List<Transactions>> GetTransactions();
        // Task<List<Transactions>> GetTransactionsTop5();
        Task<TransactionResponse?> GetTransactions(int PageIndex, int PageSize);
        Task<TransactionSummaryResponse?> GetTransactionsSummary(string customerId);
    }
}
