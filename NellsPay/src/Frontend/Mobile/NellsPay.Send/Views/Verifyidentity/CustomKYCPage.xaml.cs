using NellsPay.Send.ViewModels.Verifyidentity;

namespace NellsPay.Send.Views.Verifyidentity;

public partial class CustomKYCPage : ContentPage
{
	public CustomKYCPage(CustomKYCViewModel Viewmodel)
	{
		InitializeComponent();
		BindingContext = Viewmodel;
	}
	protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CustomKYCViewModel viewModel)
        {
            viewModel.OnPageAppearing();
        }
    }
}