using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Views.LoginPages;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PaymentsFlow;
using NellsPay.Send.Views.TransactionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.MoneyTransferFlowViewModels
{
    [QueryProperty(nameof(TransferPin), "transferPin")]
    public partial class PaymentMethodVM : BaseViewModel
    {
        #region Dependencies
        private readonly ICurrencyTransferService _currencyTransferService;
        private readonly ICustomerService _customerService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IToastService _toastService;
        #endregion

        #region Observable Properties
        [ObservableProperty] private ObservableCollection<PaymentMethod> paymentList = new();
        [ObservableProperty] private TransferPinModel transferPin;
        [ObservableProperty] private string countryCode;
        #endregion
        public PaymentMethodVM(IToastService toastService, ICurrencyTransferService currencyTransferService, ISettingsProvider settingsProvider, ICustomerService customerService)
        {
            _toastService = toastService;
            _currencyTransferService = currencyTransferService;
            _settingsProvider = settingsProvider;
            _customerService = customerService;
        }
        public async Task OnPageAppearing()
        {
            CountryCode = _currencyTransferService.Sender.Country2Code;
            LoadPaymentMethods();
        }
        private void LoadPaymentMethods()
        {
            PaymentMethod method = new();
            if (string.Equals(CountryCode, "CA", StringComparison.OrdinalIgnoreCase))
            {
                method = new PaymentMethod { Title = "Interac e-Transfer", Description = "Interac e-Transfer Method.", Icon = "banklogo.png", Selected = true };
            }

            if (UtilityHelper.UKCountries.Contains(CountryCode?.ToUpperInvariant() ?? "") || UtilityHelper.EUCountries.Contains(CountryCode?.ToUpperInvariant() ?? ""))
            {
                method = new PaymentMethod { Title = "Faster Bank Transfer", Description = "Faster Bank Transfer Method", Icon = "banklogo.png", Selected = true };
            }

            PaymentList.Add(method);

            // if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            // {
            //     method = new PaymentMethodModel
            //     {
            //         Title = "Apple Pay",
            //         Description = "Pay with Apple Pay using a linked card. Your transaction will be completed within minutes.",
            //         Icon = "applepaylogo.png",
            //         Selected = false
            //     };
            // }
            // else if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            // {
            //     method = new PaymentMethodModel
            //     {
            //         Title = "Google Pay",
            //         Description = "Pay with Google Pay using a linked card. Your transaction will be completed within minutes.",
            //         Icon = "googlepaylogo.png",
            //         Selected = false
            //     };
            // }
            // List<PaymentMethodModel> Extra = new List<PaymentMethodModel>()
            // {
            //     new PaymentMethodModel {Title = "Bank Transfer", Description="Pay with a debit/credit card you have in your Apple Pay wallet. Your transaction will be completed within minutes.", Icon="banklogo.png",Selected=false },
            //     new PaymentMethodModel {Title = "Debit/Credit Card", Description="Pay with a debit/credit card you have in your Apple Pay wallet. Your transaction will be completed within minutes.", Icon="criditlogo.png",Selected=false },
            //     new PaymentMethodModel {Title = "Bank Transfer", Description="Pay with a debit/credit card you have in your Apple Pay wallet. Your transaction will be completed within minutes.", Icon="banklogo.png",Selected=false },
            //     new PaymentMethodModel {Title = "Debit/Credit Card", Description="Pay with a debit/credit card you have in your Apple Pay wallet. Your transaction will be completed within minutes.", Icon="criditlogo.png",Selected=false },
            //     new PaymentMethodModel {Title = "ACH Transfer", Description="ACH Transfer Method.", Icon="banklogo.png",Selected=false},
            // };
            // foreach (var i in Extra)
            // {
            //     PaymentList.Add(i);
            // }

        }
        private async Task<ResponseModels.SaltbyemailResponse?> EnsureSECustomerCreatedAsync()
        {
            var saltResult = await _customerService.GetSaltbyemail(_settingsProvider.Email);

            if (saltResult == null)
            {
                var newCustomer = new AddSeCustomerWrapper
                {
                    // CustomerId = Guid.Parse(_settingsProvider.CustomerId),
                    CustomerRequest = new CustomerRequest
                    {
                        Email = _settingsProvider.Email,
                        DateOfBirth = _settingsProvider.DateOfBirth,
                        CitizenshipCode = _settingsProvider.country2Code,
                        FirstName = _settingsProvider.FullName,
                        LastName = _settingsProvider.LastName,
                        Gender = _settingsProvider.Gender,
                        PlaceOfBirth = "Belgium",
                        ResidentAddress = _settingsProvider.AddressLine
                    }
                };
                Debug.Write(newCustomer.ToString());
                var createResult = await _customerService.AddSECustomer(newCustomer, _settingsProvider.CustomerId);
                if (createResult == null)
                {
                    _toastService.ShowToast("Please Try Again, SE is not Created");
                    return null;
                }

                // Re-fetch the SE customer after creation
                saltResult = await _customerService.GetSaltbyemail(_settingsProvider.Email);
                if (saltResult == null)
                {
                    _toastService.ShowToast("Failed to fetch newly created SE customer.");
                }
            }

            return saltResult;
        }
        [RelayCommand]
        private async Task SelectPaymentMethod(PaymentMethod item)
        {
            var saltResult = await EnsureSECustomerCreatedAsync();
            _currencyTransferService.SaltbyemailResponse = saltResult;
            _currencyTransferService.SelectedPaymentMethod = item;
            await Shell.Current.GoToAsync("..");
            // if (item.Title == "Bank Transfer")
            // {
            //     await Shell.Current.GoToAsync($"{nameof(SelectBankPage)}?",
            //     new Dictionary<string, object>
            //     {
            //         ["TransferPin"] = TransferPin,
            //     });
            // }
            // else if (item.Title == "Debit Card")
            // {
            //     await Shell.Current.GoToAsync($"{nameof(SelectCardPage)}?",
            //     new Dictionary<string, object>
            //     {
            //         ["TransferPin"] = TransferPin,
            //     });
            // }
            // else if (item.Title == "Apple Pay" || item.Title == "Google Pay")
            // {
            //     //await Shell.Current.GoToAsync($"{nameof(ConfirmPaymentPage)}?",
            //     //new Dictionary<string, object>
            //     //{
            //     //    ["Transfer"] = PaymentMethod,
            //     //});
            // }
            // var user = await _customerService.GetCustomerByEmail(_settingsProvider.Email);
        }

        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task ContinueTransaction()
        {
            await Shell.Current.GoToAsync($"{nameof(TransferPinPage)}");
        }
    }
}
