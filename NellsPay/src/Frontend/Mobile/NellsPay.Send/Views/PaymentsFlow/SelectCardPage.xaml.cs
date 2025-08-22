using NellsPay.Send.ViewModels.PaymentsFlow;

namespace NellsPay.Send.Views.PaymentsFlow;

public partial class SelectCardPage : ContentPage
{
	public SelectCardPage(SelectCardVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}