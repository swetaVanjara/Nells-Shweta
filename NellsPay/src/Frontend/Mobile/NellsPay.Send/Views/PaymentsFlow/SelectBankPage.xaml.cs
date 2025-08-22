using NellsPay.Send.ViewModels.PaymentsFlow;

namespace NellsPay.Send.Views.PaymentsFlow;

public partial class SelectBankPage : ContentPage
{
	public SelectBankPage(SelectBankVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}