using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;

public partial class TransferPinPage : ContentPage
{
	public TransferPinPage(TransferPinVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}