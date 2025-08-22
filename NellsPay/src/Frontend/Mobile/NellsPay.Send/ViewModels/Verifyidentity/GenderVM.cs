using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using NellsPay.Send.Messages;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.Verifyidentity;

namespace NellsPay.Send.ViewModels.Verifyidentity
{
    public partial class GenderVM : BaseViewModel
    {
        public ObservableCollection<string> GenderList { get; } = new();

        public GenderVM()
        {
            GenderList.Add("Male");
            GenderList.Add("Female");
        }

        #region RelayCommands

        [RelayCommand]
        private async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task SelectGender(string SelectedGender)
        {
            WeakReferenceMessenger.Default.Send(new WeakMessages(SelectedGender));
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(SelectedGender));

            await Shell.Current.GoToAsync("..");
        }
        
        #endregion
    }
}