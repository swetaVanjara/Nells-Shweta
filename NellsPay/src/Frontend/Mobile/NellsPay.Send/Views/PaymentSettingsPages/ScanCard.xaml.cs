using System.Reflection.Metadata;

namespace NellsPay.Send.Views.PaymentSettingsPages;

public partial class ScanCard : ContentPage
{
	public ScanCard()
	{
		InitializeComponent();
	}
    private void FlashButton_Clicked_1(object sender, EventArgs e)
    {
        cameraReaderView.IsTorchOn = !cameraReaderView.IsTorchOn;
    }
    private int Capture = 0;
    private async void OnCaptureClicked(object sender, EventArgs e)
    {
        var image = await cameraReaderView.CaptureAsync();
        if (image != null)
        {
            if (Capture == 0)
            {
                Capture = 1;
                var stream = await image.OpenReadAsync();
                Label2.Text = "Place the backside of your card in green square";
                Label1.Text = "Step : 2/2";
            }
            else
            {
                Capture = 0;
                var stream = await image.OpenReadAsync();
                Label2.Text = "Place the frontside of your card in green square";
                Label1.Text = "Step : 1/2";
                await Shell.Current.GoToAsync("..");
            }

        }
    }

    private void photo_Clicked(object sender, EventArgs e)
    {

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}