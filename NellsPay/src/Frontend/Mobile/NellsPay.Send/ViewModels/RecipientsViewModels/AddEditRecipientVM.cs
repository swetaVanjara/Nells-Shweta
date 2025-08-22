using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui;
using NellsPay.Send.Messages;
using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Models.RecipientsModels;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PopUpPages;
using NellsPay.Send.Views.RecipientsPages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static NellsPay.Send.Messages.WeakMessages;
using PaymentMethod = NellsPay.Send.ResponseModels.PaymentMethod;


namespace NellsPay.Send.ViewModels.RecipientsViewModels
{
    [QueryProperty(nameof(RoutePageNav), "routePageNav")]
    public partial class AddEditRecipientVM : BaseViewModel
    {
        [ObservableProperty]
        private bool routePageNav;
        private readonly IRecipientService _recipientService;
        private readonly IToastService _toastService;
        private readonly RecipientDataStore _recipientDataStore;
        private readonly ICountriesService _APICountriesService;
        private readonly ICurrencyTransferService _currencyTransferService;
        private readonly IRecipientValidationService _recipientvalidationService;
        private readonly ICountriesService _Service;
        private ISettingsProvider _settingProvider { get; set; }
        [ObservableProperty] private List<CountryCodes> countries = new();
        [ObservableProperty] private List<Data> countriesData = new();
        [ObservableProperty] private Recipient recipientData;
        [ObservableProperty] private Data selectedCountryData;
        [ObservableProperty] private AddEditRecipientModel recipientInfo;
        [ObservableProperty] private bool isForEdit;
        [ObservableProperty] private string pageTitle;
        [ObservableProperty] private string bankName;
        [ObservableProperty] private string dialCode;
        [ObservableProperty] private bool isSendMoney;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private bool isNavigatingBack = false;

        public async Task OnPageAppearing()
        {
            try
            {
                WeakReferenceMessenger.Default.Register<object, ValueChangedMessage<(string Name, int CheckPayment)>>(
                this, async (r, m) =>
                {
                    if (m.Value.CheckPayment == 1 || m.Value.CheckPayment == 2)
                        await GetData(m.Value.Name, m.Value.CheckPayment);
                });
                RecipientInfo = new AddEditRecipientModel();
                if(_currencyTransferService?.ReviewTransictionData != null)
                    GetData(_currencyTransferService?.ReviewTransictionData?.DeliveryMethod,1);
                RecipientData = TempRecipientStore.AddEditRecipient.Value.Item1;
                var (recipient, type) = TempRecipientStore.AddEditRecipient.Value;
                if (TempRecipientStore.AddEditRecipient != null && type == "Edit Recipient")
                {
                    IsForEdit = true;
                    PageTitle = "Edit Recipient";

                    if (RecipientInfo != null)
                    {
                        RecipientInfo.Id = recipient.Id;
                        RecipientInfo.FirstName = recipient.FirstName;
                        RecipientInfo.LastName = recipient.LastName;
                        RecipientInfo.PhoneNumber = recipient.PhoneNumber;
                        RecipientInfo.PostalCode = recipient.PostCode;
                        RecipientInfo.Email = recipient.Email;
                        RecipientInfo.CountryName = recipient.Country;

                        // (TempRecipientStore.SelectedCountry ??= new Currency()).Country = recipient.Country;
                        int selectedIndex = CountriesData.FindIndex(c => string.Equals(c.countryName?.Trim(), recipient.Country?.Trim(), StringComparison.OrdinalIgnoreCase));
                         RecipientInfo.SelectedCountryData = CountriesData[selectedIndex];
                    }
                }
                else if (type == "New Recipient")
                {
                    IsForEdit = false;
                    PageTitle = "New Recipient";
                    RecipientData = recipient;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
        }

        public AddEditRecipientVM(
            IToastService toastService,
            ICountriesService countriesService,
            IRecipientValidationService recipientValidationService,
            IRecipientService recipientService,
            ISettingsProvider settingsProvider,
            RecipientDataStore recipientDataStore,
            ICountriesService serviceProvider,
            ICurrencyTransferService currencyTransferService)
        {
            _currencyTransferService = currencyTransferService;
            _settingProvider = settingsProvider;
            _APICountriesService = countriesService;
            _Service = serviceProvider;
            _recipientDataStore = recipientDataStore;
            _recipientService = recipientService;
            _toastService = toastService;
            _recipientvalidationService = recipientValidationService;
        }

        partial void OnRoutePageNavChanged(bool value)
        {
            IsSendMoney = value;
        }
        public async Task GetData(string Name, int CheckPayment)
        {
            if (CheckPayment == 1)
            {
                RecipientInfo.DeliveryMethod = Name;
                BankName = "";
            }
            if (CheckPayment == 2)
            {
                BankName = Name;
            }
        }
        private async void NewRecipient()
        {
            try
            {
                _recipientvalidationService.ClearStatuses(RecipientInfo);

                if (!_recipientvalidationService.Validate(RecipientInfo, _toastService))
                    return;

                RecipientData = new Recipient()
                {
                    CustomerId = Guid.Parse(_settingProvider.CustomerId),
                    Initials = $"{RecipientInfo.FirstName?.FirstOrDefault()}{RecipientInfo.LastName?.FirstOrDefault()}".ToUpper(),
                    FirstName = RecipientInfo.FirstName ?? "",
                    LastName = RecipientInfo.LastName ?? "",
                    PhoneNumber = RecipientInfo.PhoneNumber ?? "",
                    PayOutType = RecipientInfo.DeliveryMethod ?? "",
                    CountryFlag = RecipientData.CountryFlag ?? "",
                    Currency = TempRecipientStore.SelectedCountry.CurrencyCode ?? "",
                    Region = TempRecipientStore.SelectedCountry.Region,
                    Country = TempRecipientStore.SelectedCountry.Country,
                    PhoneCode = TempRecipientStore.SelectedCountry.phoneCode,
                    Email = _settingProvider.Email,
                    PostCode = RecipientInfo.PostalCode ?? "",
                    City =  "Cairo",
                    AddressLine1 = RecipientInfo.Address ?? "Nile River",
                    AddressLine2 = RecipientInfo.Address ?? "Nile River",
                    PayOutAccount = RecipientInfo.DeliveryMethod == "Bank Transfer" ? RecipientInfo.AccountNumber : RecipientInfo.PhoneNumber,
                    IsFavorite = false,
                    // = BankName,
                };

                var obj = new RecipientWrapper()
                {
                    Recipient = RecipientData
                };
                Debug.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));

                var response = await _recipientService.PostRecipientsAsync(obj);

                if (string.IsNullOrEmpty(response?.id))
                {
                    _toastService.ShowToast("Error Recipient not add");
                }
                else
                {
                    _recipientDataStore.NewlyAddedRecipient = RecipientData;
                    await Shell.Current.CurrentPage.ShowPopupAsync(new SuccessPage(
                        "Recipient added successfully",
                        "The recipient has been added successfully.",
                        "OK",
                        true
                    ));
                    if (!routePageNav)
                    {
                        string targetRoute = IsSendMoney == true ? ".." : "../..";
                        await Shell.Current.GoToAsync(targetRoute);
                    }
                    else
                    {
                        _currencyTransferService.ReviewTransictionData.Recipient = new();
                        _currencyTransferService.ReviewTransictionData.Recipient = RecipientData;
                        await Shell.Current.GoToAsync(nameof(ReviewTransictionPage));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        private async void EditRecipient()
        {
            try
            {
                if (RecipientInfo == null)
                {
                    await Shell.Current.DisplayAlert("Error", "RecipientInfo is NULL", "OK");
                    return;
                }

                if (RecipientInfo.SelectedCountryData == null)
                {
                    await Shell.Current.DisplayAlert("Error", "SelectedCountryData is NULL", "OK");
                    return;
                }

                if (!Guid.TryParse(_settingProvider.CustomerId, out var customerId))
                {
                    await Shell.Current.DisplayAlert("Error", "CustomerId is invalid", "OK");
                    return;
                }

                RecipientData = new Recipient
                {
                    Id = RecipientInfo.Id,
                    CustomerId = customerId,
                    Initials = $"{RecipientInfo.FirstName?.FirstOrDefault()}{RecipientInfo.LastName?.FirstOrDefault()}".ToUpper(),
                    FirstName = RecipientInfo.FirstName ?? "",
                    LastName = RecipientInfo.LastName ?? "",
                    Email = RecipientInfo.Email ?? "",
                    PhoneNumber = RecipientInfo.PhoneNumber ?? "",
                    PayOutType = RecipientInfo.DeliveryMethod ?? "",
                    Region = RecipientInfo.SelectedCountryData.region ?? "",
                    PostCode = RecipientInfo.PostalCode ?? "",
                    Country = RecipientInfo.SelectedCountryData.countryName ?? "",
                    CountryFlag = RecipientInfo.SelectedCountryData.countryFlag ?? "",
                    Currency = RecipientInfo.SelectedCountryData.currencyCode ?? "",
                    IsFavorite = false,
                    City = "",
                    AddressLine1 = "",
                    AddressLine2 = "",
                    PayOutAccount = RecipientInfo.DeliveryMethod == "Bank Transfer" ? RecipientInfo.AccountNumber : RecipientInfo.PhoneNumber,
                    Image = "",
                };
                var obj = new RecipientWrapper()
                {
                    Recipient = RecipientData
                };
                Console.Write(obj.ToString());
                var response = await _recipientService.PutRecipientsAsync(obj);

                if (response == null || response?.isSuccess == false)
                {
                    Shell.Current.CurrentPage.ShowPopup(new SuccessPage("Please Edit Again", "Edit Recipient failed. Please try again.", "Ok",true));
                }
                else if (response != null)
                {
                    _recipientDataStore.NewlyAddedRecipient = RecipientData;
                    Shell.Current.CurrentPage.ShowPopup(new SuccessPage("Recipient updated successfully", "Dear user your recipient has been updated successfully.", "OK",true));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Edit failed: {ex.Message}", "OK");
                Console.WriteLine("EditRecipient Error: " + ex);
            }
        }


        private List<string> GetDeliveryMethodList()
        {
            var DeliveryList = SelectedCountryData?.paymentMethods?
                .Select(pm => pm.name)
                .ToList() ?? [];
            if (DeliveryList?.Count == 0)
            {
                DeliveryList =
               [
                   
               ];

            }
            return DeliveryList;
        }

        public async Task GetCountryListData()
        {
            var data = await _APICountriesService.GetCountries(0,70);
            CountriesData = data.countries.data ?? new List<Data>();
            Debug.Write(CountriesData.ToString());
        }

        [RelayCommand]
        private async Task Delivery()
        {
            try
            {
                // var IsPageName = true;
                var PageTitle = "Choose A Delivery Method";
                await Shell.Current.GoToAsync($"{nameof(ChooseDeliveryMethodPage)}?pageTitle={PageTitle}");
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
        }

        [RelayCommand]
        private async Task Bank()
        {
            try
            {
                // var IsPageName = false;
                var PageTitle = RecipientInfo.DeliveryMethod == "Bank Transfer" ? "Choose A Bank" : "Choose Mobile Provider";
                await Shell.Current.GoToAsync($"{nameof(ChooseDeliveryMethodPage)}?pageTitle={PageTitle}");
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            NewRecipient();
        }

        [RelayCommand]
        private async Task SaveEdit()
        {
            try
            {
                if (RecipientInfo.FirstName != RecipientData.FirstName ||
               RecipientInfo.LastName != RecipientData.LastName ||
               RecipientInfo.PhoneNumber != RecipientData.PhoneNumber ||
               RecipientInfo.DeliveryMethod != RecipientData.PayOutType ||
               RecipientInfo.PostalCode != RecipientData.PostCode ||
               RecipientInfo.Email != RecipientData.Email || RecipientInfo.Email != "")
                {
                    EditRecipient();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
        }
        [RelayCommand]
        private async Task Back()
        {
            if (IsNavigatingBack) return;
            IsNavigatingBack = true;
            try
            {
                var navState = Shell.Current.CurrentState.Location.ToString();
                Console.WriteLine($"Current Navigation State: {navState}");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
            finally
            {
                IsNavigatingBack = false;
            }
        }
    }
}
