using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.TransactionViewModels
{
    [QueryProperty(nameof(TransactionDetails), "TransactionDetails")]
    public partial class TransactionDetailsVM : BaseViewModel
    {
        private readonly ICountriesService _countriesService;
        #region Fields
        [ObservableProperty] private TransactionDetailModel transactionDetails;
        [ObservableProperty] private string reciverFlag;
        [ObservableProperty] private string senderFlag;

        #endregion
        public TransactionDetailsVM(ICountriesService countriesService)
        {
            _countriesService = countriesService;
            LoadCurrencyDetailsAsync();
        }
        #region Methods
        private async Task LoadCurrencyDetailsAsync()
        {
            var countries = await _countriesService.GetCountries(0, 60);


            var matchedCountry1 = countries?.countries?.data?.FirstOrDefault(c => string.Equals(c.currencyCode, TransactionDetails.SenderCountry, StringComparison.OrdinalIgnoreCase));
            if (matchedCountry1 != null)
                SenderFlag = matchedCountry1.countryFlag;

            var matchedCountry2 = countries?.countries?.data?.FirstOrDefault(c => string.Equals(c.currencyCode, TransactionDetails.ReciverCurrency, StringComparison.OrdinalIgnoreCase));
            if (matchedCountry2 != null)
                ReciverFlag = matchedCountry2.countryFlag;
        }
        #endregion

        #region Command
        public ICommand BackCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync("..");
        });
        
        #endregion
    }
}
