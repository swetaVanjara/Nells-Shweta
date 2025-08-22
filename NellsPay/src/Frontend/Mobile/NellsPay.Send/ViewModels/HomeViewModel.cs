using CommunityToolkit.Maui.Views;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.PopUpPages;
using NellsPay.Send.Views.ProfilePages;
using NellsPay.Send.Views.Verifyidentity;
using Newtonsoft.Json;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IRecipientService _recipientService;
        private readonly ICurrencyTransferService _currencyTransferService;
        private readonly IKycService _kycService;
        private readonly ICountriesService _countriesService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IToastService _toastService;
        private readonly IFxService _fxService;
        private readonly RecipientDataStore _recipientDataStore;
        private readonly ITransactionService _transactionService;

        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private string userName;
        [ObservableProperty] private string userImage;
        [ObservableProperty] private string userInitials;
        [ObservableProperty] private string summaryTotalAmount;
        [ObservableProperty] private string summaryTransactionCount;

        [ObservableProperty] private ObservableCollection<Recipient?> recipients = new();
        [ObservableProperty] private Currency senderCurrency = new();
        [ObservableProperty] private Currency recieverCurrency = new();
        [ObservableProperty] private bool verifyIdVisible;
        [ObservableProperty] private bool sendMoneyVisible;
        [ObservableProperty] private bool newNotification = true;
        [ObservableProperty] private bool suggestedShow = true;
        public HomeViewModel(IRecipientService recipientService, ITransactionService transactionService, ICurrencyTransferService currencyTransferService, IFxService fxService, IToastService toastService, ICountriesService countriesService, IKycService kycService, ISettingsProvider settingsProvider, RecipientDataStore recipientDataStore)
        {
            _recipientService = recipientService;
            _currencyTransferService = currencyTransferService;
            _fxService = fxService;
            _toastService = toastService;
            _countriesService = countriesService;
            _kycService = kycService;
            _settingsProvider = settingsProvider;
            _recipientDataStore = recipientDataStore;
            _transactionService = transactionService;
            UserName = _settingsProvider.UserName;
            UserImage = "usertest.png";
            UserInitials = $"{_settingsProvider.FullName?.FirstOrDefault()}{_settingsProvider.UserName?.FirstOrDefault()}".ToUpper();
            Recipients = new ObservableCollection<Recipient?>();
            Task.Run(async () => { await GetData(); });
        }

        public async void updateIdentity()
        {
            if (_recipientDataStore.NewlyAddedRecipient != null)
            {
                Recipients.Insert(0, _recipientDataStore.NewlyAddedRecipient);
                Recipients = new ObservableCollection<Recipient?>(Recipients.Take(5));
                _recipientDataStore.NewlyAddedRecipient = null;
            }
            await LoadRecipientsAsync();
        }


        #region Methods
        public async Task GetData()
        {
            try
            {
                await LoadRecipientsAsync();
                await LoadProfileStatusAsync();
                await LoadCurrencyDetailsAsync();
                await LoadSummaryDetailsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HomeViewModel.GetData] Error: {ex.Message}");
            }
        }
        private async Task LoadSummaryDetailsAsync()
        {
            var response = await _transactionService.GetTransactionsSummary(_settingsProvider.CustomerId);
            if (response != null)
            {
                foreach (var item in response.TransactionSummary.CurrencySummaries)
                {
                    SummaryTotalAmount = $"{item.CurrencySymbol}{item.TotalAmount:N0}";
                    SummaryTransactionCount = item.TransactionCount.ToString();
                }
            }
        }

        private async Task LoadRecipientsAsync()
        {
            var response = await _recipientService.GetRecipientsAsync();
            if (response?.Recipients != null)
            {
                foreach (var item in response.Recipients)
                    item.Initials = item.Initials?.ToUpper();

                Recipients = new ObservableCollection<Recipient?>(response.Recipients.Take(5));
            }
        }

        private async Task LoadProfileStatusAsync()
        {
            var profile = await _kycService.GetProfilebyId(_settingsProvider.CustomerId);
            if (profile?.Profile is null) return;
            SendMoneyVisible = string.Equals(profile.Profile.ComplianceStatus, "Approved", StringComparison.OrdinalIgnoreCase);
            VerifyIdVisible = !SendMoneyVisible;
            _settingsProvider.Verifyidentity = !VerifyIdVisible;
        }

        private async Task LoadCurrencyDetailsAsync()
        {
            try
            {
                var countriesResponse = await _countriesService.GetCountries(0, 60);
                var allCountries = countriesResponse?.countries?.data ?? [];

                var nationality = _settingsProvider?.CustomerId is not null ? _settingsProvider.country2Code : null;

                var matchedCountry = allCountries?.FirstOrDefault(c => c.country2Code == nationality) ??
                                    allCountries?.FirstOrDefault(c => c.country2Code == "FR");

                if (matchedCountry != null)
                    SenderCurrency = MapCountryToCurrency(matchedCountry);

                var cameroon = allCountries?.FirstOrDefault(c => c.country2Code == "CM");
                if (cameroon != null)
                    RecieverCurrency = MapCountryToCurrency(cameroon);

                var fxResult = await _fxService.ConvertCurrency(SenderCurrency.CurrencyCode, RecieverCurrency.CurrencyCode, 1.0, false);

                var ConvertCurrencyData = fxResult.FxConvert;

                if (ConvertCurrencyData != null)
                {
                    SenderCurrency.Region = $"{Math.Round((decimal)ConvertCurrencyData.Amount)} {SenderCurrency.CurrencyCode}";
                    RecieverCurrency.Region = $"{Math.Round((decimal)ConvertCurrencyData.ConvertedAmount, 2):0.00} {RecieverCurrency.CurrencyCode}";
                }
                _currencyTransferService.Reciever = RecieverCurrency;
                _currencyTransferService.Sender = SenderCurrency;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HomeViewModel.LoadCurrencyDetailsAsync] Error: {ex.Message}");
            }

        }

        private static Currency MapCountryToCurrency(Data? c) => new()
        {
            Id = Guid.Parse(c?.id ?? Guid.NewGuid().ToString()),
            Country = c?.countryName ?? "",
            CurrencyCode = c?.currencyCode ?? "",
            Country2Code = c?.country2Code ?? "",
            Country3Code = c?.country3Code ?? "",
            Region = c?.region ?? "",
            CurrencyName = c?.currencyName ?? "",
            paymentMethods = c?.paymentMethods ?? [],
            CountryFlag = c?.countryFlag ?? "",
            CurrencySymbol = c?.currencySymbol ?? "",
            CurrencyFlag = c?.currencyFlag ?? ""
        };

        #endregion
        #region RelayCommands
        [RelayCommand]
        private async Task Notification() => await Shell.Current.GoToAsync("//HomePage/NotificatiosPage");

        [RelayCommand]
        private async Task Verify()
        {
            try
            {
                IsLoading = true;
                var session = await _kycService.GetSessionByProfileId(_settingsProvider.ProfileId);

                if (session?.Title == "Not Found")
                {
                    var newSession = new SessionWrapper
                    {
                        Session = new Session
                        {
                            Callback = new Uri("https://www.nellspay.com"),
                            VendorData = _settingsProvider.ProfileId
                        }
                    };

                    var response = await _kycService.PostSessionStart(newSession);
                    if (response?.Url != null)
                    {
                        _settingsProvider.Session = JsonConvert.SerializeObject(newSession);
                        // await Shell.Current.GoToAsync("//HomePage/SelectDocumentPage");
                        await Shell.Current.GoToAsync("//HomePage/SelectCountryRecipient");
                    }
                    else
                    {
                        _toastService.ShowToast($"{_settingsProvider.ProfileId} Session not started. Please try again.");
                    }
                }
                else
                {
                    // await Shell.Current.GoToAsync("//HomePage/SelectDocumentPage");
                    await Shell.Current.GoToAsync($"//HomePage/SelectCountryRecipient?pageName={Uri.EscapeDataString("Verify")}");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[VerifyCommand Error]: {e.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task UserEdit()
        {
            await Shell.Current.GoToAsync(nameof(EditProfilePage));
        }

        [RelayCommand]
        private Task CurrencyOption() => Task.CompletedTask; // Placeholder

        [RelayCommand]
        private Task SendNow() => Shell.Current.GoToAsync("//SendMoneyPage");

        [RelayCommand]
        private void SuggestedClose() => SuggestedShow = false;

        [RelayCommand]
        private Task ReferNow() => Task.CompletedTask; // Placeholder

        [RelayCommand]
        private Task SeeAll()
        {
            return Shell.Current.GoToAsync($"//RecipientPage?isFromMoney=false");
        }
        #endregion
    }
}
