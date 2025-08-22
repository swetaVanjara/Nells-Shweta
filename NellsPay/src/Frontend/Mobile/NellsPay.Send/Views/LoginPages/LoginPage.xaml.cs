using NellsPay.Send.ViewModels.LoginViewModels;

namespace NellsPay.Send.Views.LoginPages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}