using NellsPay.Send.ViewModels.RecipientsViewModels;

namespace NellsPay.Send.Views.RecipientsPages;

public partial class SelectCountryRecipient : ContentPage
{
	public SelectCountryRecipient(SelectCountryRecipientVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SelectCountryRecipientVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
}