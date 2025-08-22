using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;

public partial class ChooseReasonPage : ContentPage
{
	public ChooseReasonPage(ChooseReasonVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ChooseReasonVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
}