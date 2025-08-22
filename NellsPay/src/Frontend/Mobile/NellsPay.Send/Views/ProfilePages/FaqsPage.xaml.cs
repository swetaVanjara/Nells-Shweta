using NellsPay.Send.ViewModels.ProfileViewModels;

namespace NellsPay.Send.Views.ProfilePages;

public partial class FaqsPage : ContentPage
{
	public FaqsPage(FaqsVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}