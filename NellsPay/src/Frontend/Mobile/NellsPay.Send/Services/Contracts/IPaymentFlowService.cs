using NellsPay.Send.Models.PamentFlow;
using NellsPay.Send.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Services.Contracts
{
    public interface IPaymentFlowService
    {
        Task<PaymentsProvidersResponse> GetPaymentProvider(string countryCode, int PageIndex, int PageSize);
        Task<PaymentWrapper> CreatePayment(PaymentInitiationWrapper paymentInitiationWrapper);
        Task<PaymentStatusResponse?> GetPaymentStatus(string paymentId);
        Task<List<BankAccountModel>> GetAvailableBankAccounts(string bankId);
        Task<bool> AddBankAccount(BankAccountModel bankAccount);
        Task<List<CardsModel>> GetAvailableCards();
        Task<List<BankModel>> GetAvailableBanks();
    }
}
