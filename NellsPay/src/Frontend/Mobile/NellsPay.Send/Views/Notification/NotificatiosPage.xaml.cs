using NellsPay.Send.ViewModels.Notification;

namespace NellsPay.Send.Views.Notification;

public partial class NotificatiosPage : ContentPage
{
	public NotificatiosPage(NotificatiosVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}