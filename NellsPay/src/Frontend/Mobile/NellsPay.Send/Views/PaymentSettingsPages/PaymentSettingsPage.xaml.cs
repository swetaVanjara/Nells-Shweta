using NellsPay.Send.ViewModels.PaymentSettingsViewModels;

namespace NellsPay.Send.Views.PaymentSettingsPages;

public partial class PaymentSettingsPage : ContentPage
{
	public PaymentSettingsPage(PaymentSettingsVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}