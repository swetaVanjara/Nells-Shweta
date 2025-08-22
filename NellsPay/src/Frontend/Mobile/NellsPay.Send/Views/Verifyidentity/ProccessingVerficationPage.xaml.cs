using NellsPay.Send.ViewModels.Verifyidentity;

namespace NellsPay.Send.Views.Verifyidentity;

public partial class ProccessingVerficationPage : ContentPage
{
	public ProccessingVerficationPage(ProccessingVerficationVM vM)
	{
		InitializeComponent();
		BindingContext = vM;
    }
}