using NellsPay.Send.ViewModels.PaymentsFlow;

namespace NellsPay.Send.Views.PaymentsFlow;

public partial class SelectBankAccountPage : ContentPage
{
	public SelectBankAccountPage(SelectBankAccountVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}