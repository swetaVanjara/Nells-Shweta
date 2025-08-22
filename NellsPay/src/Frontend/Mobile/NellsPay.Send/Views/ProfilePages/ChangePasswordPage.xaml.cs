using NellsPay.Send.ViewModels.ProfileViewModels;

namespace NellsPay.Send.Views.ProfilePages;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage(ChangePasswordVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}