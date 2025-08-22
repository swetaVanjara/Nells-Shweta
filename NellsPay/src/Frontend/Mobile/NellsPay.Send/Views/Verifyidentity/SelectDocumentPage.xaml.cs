using NellsPay.Send.ViewModels.Verifyidentity;
using System.Threading.Tasks;

namespace NellsPay.Send.Views.Verifyidentity;

public partial class SelectDocumentPage : ContentPage
{
	public SelectDocumentPage(SelectDocumentVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SelectDocumentVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        // await Navigation.PushModalAsync(new NavigationPage(new CameraPage()));
    }
}