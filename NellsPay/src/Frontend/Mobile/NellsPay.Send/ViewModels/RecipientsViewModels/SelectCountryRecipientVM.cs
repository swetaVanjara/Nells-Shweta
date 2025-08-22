using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.RecipientsPages;
using NellsPay.Send.Views.Verifyidentity;

namespace NellsPay.Send.ViewModels.RecipientsViewModels
{
    [QueryProperty(nameof(RoutePageNav), "routePageNav")]
    [QueryProperty(nameof(PageName), "pageName")]
    public partial class SelectCountryRecipientVM : BaseViewModel
    {
        private readonly ICountriesService _APICountriesService;
        [ObservableProperty] private bool routePageNav;
        [ObservableProperty] private string titlePage;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private bool isBackButton;
        [ObservableProperty] private string pageName;
        [ObservableProperty] private Currency currencySelected = new();
        [ObservableProperty] private ObservableCollection<Currency> currencyList = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasFavorites))]
        private ObservableCollection<Currency> favorites = new();
        CountriesResponse? data;
        public bool HasFavorites => Favorites?.Any() == true;
        partial void OnFavoritesChanged(ObservableCollection<Currency> value)
        {
            if (value != null)
                value.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasFavorites));
        }

        partial void OnPageNameChanged(string value)
        { 
            if (string.Equals(PageName, "Verify", StringComparison.OrdinalIgnoreCase))
                TitlePage = "Select Country Of Residence";
        }
        public SelectCountryRecipientVM(ICountriesService countriesService)
        {
            _APICountriesService = countriesService;
            CurrencyList = new ObservableCollection<Currency>();
            Task.Run(async () =>
            {
                await GetData();
            });
        }
        public async Task OnPageAppearing()
        {
        }
        public async Task GetData()
        {
            try
            {
                TitlePage = "Recipient's Country";
                IsLoading = true;
                List<Currency> currencies = new();
                data = await _APICountriesService.GetCountries(0, 60);
                if (data != null)
                {
                    foreach (var item in data.countries.data)
                    {
                        currencies.Add(new Currency
                        {
                            Id = Guid.Parse(item.id),
                            Country = item.countryName,
                            CurrencyCode = item.currencyCode,
                            Country2Code = item.country2Code,
                            Country3Code = item.country3Code,
                            Region = item.region,
                            CountryFlag = item.countryFlag,
                            CurrencyFlag = item.currencyFlag,
                            paymentMethods = item.paymentMethods,
                            CurrencyName = item.currencyName,
                            CurrencySymbol = item.currencySymbol,
                            phoneCode = item.phoneCode,
                            IsFavorite = item.isFavCountry,
                        }
                        );
                    }
                }
                CurrencyList = new ObservableCollection<Currency>(currencies);
                Favorites = new ObservableCollection<Currency>(currencies.Where(x => x.IsFavorite).ToList());
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
            finally
            {
                IsLoading = false;
            }
        }
        partial void OnRoutePageNavChanged(bool value)
        {
            IsBackButton = value;
        }

        #region RelayCommands

        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task Search() { }

        [RelayCommand]
        private async Task Ok()
        {
            if (TitlePage == "Sending Currency")
            {
                await Shell.Current.GoToAsync($"..?",
              new Dictionary<string, object>
              {
                  ["CurrencyChange"] = CurrencySelected,
                  ["ChangedCurrencyCheck"] = "Send",
              });
            }
            else if (TitlePage == "Receiving Currency")
            {
                await Shell.Current.GoToAsync($"..?",
              new Dictionary<string, object>
              {
                  ["CurrencyChange"] = CurrencySelected,
                  ["ChangedCurrencyCheck"] = "Recive",
              });
            }
        }

        [RelayCommand]
        private async Task ChooseRecivingCurrency(Currency item)
        {
            if (string.Equals(PageName, "Verify", StringComparison.OrdinalIgnoreCase))
            { 
                var json = JsonSerializer.Serialize(item);
                var encoded = Uri.EscapeDataString(json);
                await Shell.Current.GoToAsync($"{nameof(CustomKYCPage)}?currencyItemJson={encoded}");
            }
            else
            {
                var obj = new Recipient()
                {
                    CountryFlag = item.CountryFlag,
                    Country = item.Country,
                    Region = item.Region,
                    PhoneCode = item.phoneCode,
                };
                TempRecipientStore.AddEditRecipient = (obj, "New Recipient");
                TempRecipientStore.PaymentMethodList = item.paymentMethods;
                TempRecipientStore.SelectedCountry = item;
                await Shell.Current.GoToAsync(nameof(AddEditRecipientPage) + $"?routePageNav={IsBackButton}");
            }
        }

        [RelayCommand]
        private async Task ChooseFavoritesCurrency(Currency item)
        {
            try
            {
                if (item != null)
                {
                    // call the method to update the IsFavorite property
                    item.IsFavorite = !item.IsFavorite;
                    var selectedItem = data.countries.data
                            .FirstOrDefault(c => c.id == item.Id.ToString());
                    selectedItem.isFavCountry = item.IsFavorite;
                    await _APICountriesService.ToggleFavCountry(selectedItem);
                    Favorites = new ObservableCollection<Currency>(CurrencyList.Where(x => x.IsFavorite).ToList());
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        #endregion
    }
}