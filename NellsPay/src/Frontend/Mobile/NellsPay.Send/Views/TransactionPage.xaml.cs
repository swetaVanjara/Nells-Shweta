using NellsPay.Send.ViewModels;

namespace NellsPay.Send.Views;

public partial class TransactionPage : ContentPage
{
	public TransactionPage(TransactionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}