using NellsPay.Send.ViewModels.RecipientsViewModels;

namespace NellsPay.Send.Views.RecipientsPages;

public partial class ChooseDeliveryMethodPage : ContentPage
{
	public ChooseDeliveryMethodPage(ChooseDeliveryMethodVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ChooseDeliveryMethodVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
}