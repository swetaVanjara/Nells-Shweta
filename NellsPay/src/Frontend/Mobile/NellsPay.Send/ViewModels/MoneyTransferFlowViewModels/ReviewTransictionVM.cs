using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PaymentsFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.MoneyTransferFlowViewModels
{
    public partial class ReviewTransictionVM : BaseViewModel
    {
        #region Dependencies
        private readonly ICurrencyTransferService _currencyTransferService;
        private readonly IToastService _toastService;
        private readonly ICustomerService _customerService;
        private readonly ISettingsProvider _settingsProvider;
        #endregion

        #region Observable Properties
        [ObservableProperty] private Recipient selectedRecipient;
        [ObservableProperty] private ReviewTransictionModel reviewTransaction = new();
        [ObservableProperty] private PaymentMethod paymentMethod = new();
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private bool isPayNow;
        [ObservableProperty] private string reason;
        

        #endregion

        public ReviewTransictionVM(
         IToastService toastService,
         ICurrencyTransferService currencyTransferService,
         ISettingsProvider settingsProvider,
         ICustomerService customerService)
        {
            _toastService = toastService;
            _currencyTransferService = currencyTransferService;
            _settingsProvider = settingsProvider;
            _customerService = customerService;

            ReviewTransaction = _currencyTransferService.ReviewTransictionData!;
            SelectedRecipient = ReviewTransaction.Recipient;
            UpdatePayNowEligibility();
        }

        #region Commands
        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private void EditAmount() { }

        [RelayCommand]
        private async Task ChangeAmount()
        {
            await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task ChangeRecipient()
        {
            await Shell.Current.GoToAsync($"Recipients/Money?isFromMoney=true");
        }

        [RelayCommand]
        private async Task PaymentMethodNavigate()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PaymentMethodPage));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
         [RelayCommand]
        private async Task ChooseReason()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ChooseReasonPage));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [RelayCommand]
        private async Task PayNow() => await ProcessTransactionAsync();

        #endregion

        #region Methods

        partial void OnPaymentMethodChanged(PaymentMethod value)
        {
            UpdatePayNowEligibility();
        }

        partial void OnReasonChanged(string value)
        {
            UpdatePayNowEligibility();
        }

        private void UpdatePayNowEligibility()
        {
            IsPayNow =
                !string.IsNullOrWhiteSpace(Reason) &&
                !string.IsNullOrWhiteSpace(PaymentMethod?.Title);
        }
        public async Task OnPageAppearing()
        {
            try
            {
                if (!string.IsNullOrEmpty(_currencyTransferService?.SelectedPaymentMethod?.Title))
                { 

                    PaymentMethod = new PaymentMethod
                    {
                        Title = _currencyTransferService.SelectedPaymentMethod.Title ?? ""
                    };
                }
                if (!string.IsNullOrEmpty(_currencyTransferService?.sendingReasonModel?.ReasonName))
                    Reason = _currencyTransferService.sendingReasonModel.ReasonName;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private async Task ProcessTransactionAsync()
        {
            try
            {
                IsLoading = true;
                var saltResult = _currencyTransferService.SaltbyemailResponse;
                if (saltResult != null)
                {
                    ReviewTransaction.Reason = Reason;
                    var transactionWrapper = BuildTransactionWrapper(saltResult?.SeCustomerId);
                    var transactionResult = await _customerService.CreateTransaction(transactionWrapper);
                    if (transactionResult != null)
                    {
                        ReviewTransaction.TransactionNumber = transactionResult.TransactionNumber;
                        ReviewTransaction.Id = new Guid(transactionResult.Id);

                        await Shell.Current.GoToAsync($"{nameof(SelectBankPage)}?transactionNumber={transactionResult.TransactionNumber}&customerSeId={saltResult?.SeCustomerId}");
                    }
                }
                else
                {
                    _toastService.ShowToast("Please Try Again, SE is not Created");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private CreateTransactionWrapper BuildTransactionWrapper(string? seCustomerId)
        {
            return new CreateTransactionWrapper
            {
                Transaction = new()
                {
                    UserId = _settingsProvider.UserId,
                    CustomerId = _settingsProvider.CustomerId,
                    Email = _settingsProvider.Email,
                    FirstName = _settingsProvider.FullName,
                    LastName = _settingsProvider.LastName,
                    PhoneNumber = _settingsProvider.PhoneNumber,
                    SenderState = _settingsProvider.Region,
                    SenderZipCode = _settingsProvider.PostCode,
                    SenderAddress = _settingsProvider.LastName,
                    SenderCity = _settingsProvider.City,
                    SenderCountry = _settingsProvider.Country,
                    SenderCountryCode = _settingsProvider.country2Code,
                    SenderAmount = ReviewTransaction.Transferamount,
                    SenderCurrency = _currencyTransferService.Sender.CurrencyCode,
                    ExchangeRate = ReviewTransaction.Exchangerate,
                    PaymentMethod = "BankTransfer",
                    PayOutAccount = ReviewTransaction.Recipient.PhoneNumber,
                    PayOutMethod = "2",
                    ReceivedAmount = ReviewTransaction.Reciveamount,
                    RecipientAddress = ReviewTransaction.Recipient.AddressLine1,
                    RecipientCurrency = _currencyTransferService.Reciever.CurrencyCode,
                    RecipientCity = ReviewTransaction.Recipient.City,
                    RecipientCountry = ReviewTransaction.Recipient.Country,
                    RecipientCountryCode = _currencyTransferService.Reciever?.CurrencyCode,
                    RecipientFirstName = ReviewTransaction.Recipient.FirstName,
                    RecipientLastName = ReviewTransaction.Recipient.LastName,
                    RecipientPhoneNumber = ReviewTransaction.Recipient.PhoneNumber,
                    RecipientState = "",
                    RecipientZipCode = ReviewTransaction.Recipient.PostCode,
                    SeCustomerId = seCustomerId,
                    TransactionFee = 0,
                    TransactionReason = ReviewTransaction.Reason,
                    Expiration = "",
                    CardName = "",
                    CardNumber = "",
                    CouponCode = "",
                    Cvv = "",
                }
            };
        }
        #endregion
    }
}