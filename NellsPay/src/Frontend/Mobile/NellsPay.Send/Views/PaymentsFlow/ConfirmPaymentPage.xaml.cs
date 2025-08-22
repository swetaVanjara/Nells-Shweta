using NellsPay.Send.ViewModels.PaymentsFlow;

namespace NellsPay.Send.Views.PaymentsFlow;

public partial class ConfirmPaymentPage : ContentPage
{
	public ConfirmPaymentPage(ConfirmPaymentVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}