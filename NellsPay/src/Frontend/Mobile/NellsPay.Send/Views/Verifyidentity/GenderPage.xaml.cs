using NellsPay.Send.ViewModels.Verifyidentity;

namespace NellsPay.Send.Views.Verifyidentity;

public partial class GenderPage : ContentPage
{
	public GenderPage(GenderVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}