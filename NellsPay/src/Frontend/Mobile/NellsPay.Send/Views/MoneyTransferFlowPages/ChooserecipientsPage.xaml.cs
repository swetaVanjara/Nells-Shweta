using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;

public partial class ChooserecipientsPage : ContentPage
{
	public ChooserecipientsPage(ChooserecipientsVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}