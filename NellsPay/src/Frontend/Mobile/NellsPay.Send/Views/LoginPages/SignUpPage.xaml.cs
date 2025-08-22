using NellsPay.Send.ViewModels.LoginViewModels;

namespace NellsPay.Send.Views.LoginPages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}