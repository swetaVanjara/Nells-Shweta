using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;

public partial class ReviewTransictionPage : ContentPage
{
	public ReviewTransictionPage(ReviewTransictionVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ReviewTransictionVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
}