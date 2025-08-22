using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;

public partial class PaymentMethodPage : ContentPage
{
	public PaymentMethodPage(PaymentMethodVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PaymentMethodVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
}