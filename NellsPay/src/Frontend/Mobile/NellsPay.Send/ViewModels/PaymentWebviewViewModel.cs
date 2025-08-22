using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PopUpPages;

namespace NellsPay.Send.ViewModels
{
    [QueryProperty(nameof(UrlWebView), "urlWebView")]
    [QueryProperty(nameof(PaymentId), "paymentId")]
    public partial class PaymentWebViewModel : BaseViewModel
    {
        private readonly IPaymentFlowService _paymentService;
        [ObservableProperty] private string urlWebView;
        [ObservableProperty] private string paymentId;

        public PaymentWebViewModel(IPaymentFlowService paymentFlowService)
        {
            _paymentService = paymentFlowService;
        }

        public async Task GetPaymentStatus()
        {
            var CheckPayment = await _paymentService.GetPaymentStatus(PaymentId);
            if (CheckPayment != null)
            {
                var status = CheckPayment.paymentStatuses?
                        .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.status))?
                        .status;
               
                switch (status?.ToLowerInvariant())
                {
                    case "initiated":
                        await Shell.Current.GoToAsync(nameof(TransferPinPage));
                        break;
                    case "settled":
                        await Shell.Current.GoToAsync(nameof(TransferPinPage));
                        break;
                    default:
                        await Shell.Current.CurrentPage.ShowPopupAsync(
                            new SuccessPage("Payment Failed", "Your Payment is Not Completed", "Okay", false));
                        Application.Current.MainPage = new AppShell();
                        break;
                }
            }
        }

        [RelayCommand]
        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
