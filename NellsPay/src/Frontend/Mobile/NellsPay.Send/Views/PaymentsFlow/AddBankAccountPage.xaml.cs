using NellsPay.Send.ViewModels.PaymentsFlow;

namespace NellsPay.Send.Views.PaymentsFlow;

public partial class AddBankAccountPage : ContentPage
{
	public AddBankAccountPage(AddBankAccountVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}