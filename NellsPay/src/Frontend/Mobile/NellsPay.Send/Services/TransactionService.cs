using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;
using Refit;

namespace NellsPay.Send.Services
{
    public class TransactionService(IUserService userService, IToastService toastService) : BaseService, ITransactionService
    {
        private readonly ITransactionAPI _transactionApi = HttpClientProvider.Instance.GetApi<ITransactionAPI>();
        private readonly IToastService _toastService = toastService;

        public async Task<TransactionResponse?> GetTransactions(int PageIndex, int PageSize)
        {
            try
            {
                await userService.RefreshToken();
                var transaction = await _transactionApi.GetTransactions(Auth, PageIndex, PageSize);
                return transaction;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<TransactionSummaryResponse?> GetTransactionsSummary(string customerId)
        {
            try
            {
                await userService.RefreshToken();
                var transaction = await _transactionApi.GetTransactionsSummary(Auth,customerId);
                return transaction;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }

    }
}
