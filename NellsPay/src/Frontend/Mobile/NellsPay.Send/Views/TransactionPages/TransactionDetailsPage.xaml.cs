using NellsPay.Send.ViewModels.TransactionViewModels;

namespace NellsPay.Send.Views.TransactionPages;

public partial class TransactionDetailsPage : ContentPage
{
	public TransactionDetailsPage(TransactionDetailsVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}