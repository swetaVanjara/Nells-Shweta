using NellsPay.Send.Models.PamentFlow;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Services
{
    public class PaymentFlowService(ISettingsProvider settingsProvider, IUserService userService, IToastService toastService) : BaseService, IPaymentFlowService
    {
        private readonly IPaymentAPI _paymentAPI = HttpClientProvider.Instance.GetApi<IPaymentAPI>();
        private readonly IToastService _toastService = toastService;
        public async Task<PaymentsProvidersResponse> GetPaymentProvider(string countryCode, int PageIndex, int PageSize)
        {
            try
            {
                await userService.RefreshToken();
                return await _paymentAPI.GetPaymentProvider(Auth, countryCode, PageIndex, PageSize);
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }
        public async Task<PaymentWrapper> CreatePayment(PaymentInitiationWrapper paymentInitiationWrapper)
        {
            try
            {
                await userService.RefreshToken();
                var payment = await _paymentAPI.CreatePayment(Auth, paymentInitiationWrapper);
                return payment;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }
        
        public async Task<PaymentStatusResponse?> GetPaymentStatus(string paymentId)
        {
            try
            {
                await userService.RefreshToken();
                var paymentStatus = await _paymentAPI.GetPaymentStatus(Auth, paymentId);
                return paymentStatus;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }

        public Task<bool> AddBankAccount(BankAccountModel bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<List<BankAccountModel>> GetAvailableBankAccounts(string bankId)
        {
            return Task.FromResult(new List<BankAccountModel>()
            {
                new BankAccountModel
                {
                    Bank = new BankModel { Id = "1", BankName = "Bank A", Image = "debit.png" },
                    AccountNumber = "123456789",
                    AccountHolderName = "John Doe"
                },
                new BankAccountModel
                {
                    Bank = new BankModel { Id = "2", BankName = "Bank B", Image = "debit.png"  },
                    AccountNumber = "987654321",
                    AccountHolderName = "Jane Smith"
                }
            });
        }

        public Task<List<BankModel>> GetAvailableBanks()
        {
            return Task.FromResult(new List<BankModel>()
            {
                new BankModel { Id = "1", BankName = "Bank A", Image = "debit.png" },
                new BankModel { Id = "2", BankName = "Bank B", Image = "debit.png"  },
                new BankModel { Id = "3", BankName = "Bank C", Image = "debit.png"  }
            });
        }

        public Task<List<CardsModel>> GetAvailableCards()
        {
            return Task.FromResult(new List<CardsModel>()
            {
                new CardsModel { CardNumber = "1234 5678 9012 3456", ExpiredYear = "2025", ExpiredMonth = "12", CardHolderName = "John Doe" },
                new CardsModel { CardNumber = "9876 5432 1098 7654", ExpiredYear = "2024", ExpiredMonth = "11", CardHolderName = "Jane Smith" }
            });
        }
    }
}

