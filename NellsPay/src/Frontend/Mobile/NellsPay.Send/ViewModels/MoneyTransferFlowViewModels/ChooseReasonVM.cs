using NellsPay.Send.Models.MoneyTransferFlowModels;

namespace NellsPay.Send.ViewModels.MoneyTransferFlowViewModels
{
    public partial class ChooseReasonVM : BaseViewModel
    {
        #region Dependencies
        private readonly ICurrencyTransferService _currencyTransferService;
        #endregion
        #region Observable Properties
        [ObservableProperty] private ReviewTransictionModel reviewTransaction = new();
        [ObservableProperty] private List<SendingReasonModel> sendingReason  = new();
        
        #endregion
        public ChooseReasonVM(ICurrencyTransferService currencyTransferService)
        {
            _currencyTransferService = currencyTransferService;
            SendingReason = _currencyTransferService.ReviewTransictionData!.SendingReasons;
        }

        public async Task OnPageAppearing()
        {

        }

        #region Commands
        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task SelectReason(SendingReasonModel item)
        {
            _currencyTransferService.sendingReasonModel = item;
            await Shell.Current.GoToAsync("..");
        }
        
        #endregion
    }
}