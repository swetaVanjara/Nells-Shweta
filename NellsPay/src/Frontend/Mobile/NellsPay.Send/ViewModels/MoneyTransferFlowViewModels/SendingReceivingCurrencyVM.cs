using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static NellsPay.Send.Messages.WeakMessages;

namespace NellsPay.Send.ViewModels.SendingReceivingCurrencyVM
{
    [QueryProperty(nameof(CurrencySelected), "currency")]
    [QueryProperty(nameof(TitlePage), "title")]
    public partial class SendingReceivingCurrencyVM : BaseViewModel
    {
        private readonly ICountriesService _countriesService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasFavorites))]
        private ObservableCollection<Currency> favorites = new();
        CountriesResponse? data;
        [ObservableProperty] private ObservableCollection<Currency> currencyList = new();


        public bool HasFavorites => Favorites?.Any() == true;
        partial void OnFavoritesChanged(ObservableCollection<Currency> value)
        {
            if (value != null)
                value.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasFavorites));
        }


        [ObservableProperty] private Currency currencySelected = new();
        [ObservableProperty] private bool isLoading;
        // [ObservableProperty] private string search;
        [ObservableProperty] private string titlePage;

        public SendingReceivingCurrencyVM(ICountriesService countriesService)
        {
            _countriesService = countriesService;
            CurrencyList = new ObservableCollection<Currency>();
            Task.Run(async () =>
            {
                await GetData();
            });
        }


        #region Methods
        public async Task GetData()
        {
            try
            {
                IsLoading = true;
                List<Currency> currencies = new();
                data = await _countriesService.GetCountries(0, 60);
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
                            IsFavorite = item.isFavCurrency,
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
        #endregion
        #region RelayCommands

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
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task ChooseRecivingCurrency(Currency item)
        {
            if (item != null)
            {
                if (TitlePage == "Sending Currency")
                {
                    await Shell.Current.GoToAsync("..", new Dictionary<string, object>
                    {
                        ["ChangedCurrencyCheck"] = "Send",
                        ["CurrencyChange"] = item
                    });
                }
                else if (TitlePage == "Receiving Currency")
                {
                    await Shell.Current.GoToAsync("..", new Dictionary<string, object>
                    {
                        ["ChangedCurrencyCheck"] = "Receive",
                        ["CurrencyChange"] = item
                    });
                }
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
                    selectedItem.isFavCurrency = item.IsFavorite;
                    await _countriesService.ToggleFavCurrency(selectedItem);
                    if (item.IsFavorite)
                    {
                        if (!favorites.Any(f => f.Id == item.Id))
                        {
                            favorites.Add(item);
                        }
                    }
                    else
                    {
                        var toRemove = favorites.FirstOrDefault(f => f.Id == item.Id);
                        if (toRemove != null)
                        {
                            favorites.Remove(toRemove);
                        }
                    }
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
