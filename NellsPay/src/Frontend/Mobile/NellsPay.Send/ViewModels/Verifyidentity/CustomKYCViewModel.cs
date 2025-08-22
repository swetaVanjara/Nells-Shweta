using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.Verifyidentity;
using static NellsPay.Send.Messages.WeakMessages;

namespace NellsPay.Send.ViewModels.Verifyidentity
{
    [QueryProperty(nameof(CurrencyItemJson), "currencyItemJson")]
    public partial class CustomKYCViewModel : BaseViewModel
    {
        [ObservableProperty] private Currency currencyItem;
        [ObservableProperty] private string currencyItemJson;
        [ObservableProperty] private string address;
        [ObservableProperty] private string city;
        [ObservableProperty] private string postCode;
        [ObservableProperty] private string phoneNumber;
        [ObservableProperty] private string gender;

        partial void OnCurrencyItemJsonChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var decoded = Uri.UnescapeDataString(value);
                CurrencyItem = JsonSerializer.Deserialize<Currency>(decoded);
            }
        }

        public CustomKYCViewModel()
        {
            Gender = Gender ?? "Gender";
        }
        public async Task OnPageAppearing()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>( this, (r, m) =>
            {
                if (!string.IsNullOrEmpty(m.Value))
                    Gender = m.Value;
            });
        }

        #region RelayCommands

        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task Continue()
        {
            await Shell.Current.GoToAsync(nameof(SelectDocumentPage));
        }
        [RelayCommand]
        private async Task SelectGender()
        {
            await Shell.Current.GoToAsync(nameof(GenderPage));
        }

        #endregion
    }
}