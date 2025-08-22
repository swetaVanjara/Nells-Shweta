using NellsPay.Send.ViewModels.PaymentSettingsViewModels;

namespace NellsPay.Send.Views.PaymentSettingsPages;

public partial class AddManuallyPage : ContentPage
{
	public AddManuallyPage(AddManuallyVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}