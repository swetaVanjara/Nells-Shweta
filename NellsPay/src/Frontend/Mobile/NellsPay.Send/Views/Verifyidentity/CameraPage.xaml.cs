using NellsPay.Send.ViewModels.LoginViewModels;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace NellsPay.Send.Views.Verifyidentity;


public partial class CameraPage : ContentPage
{
	public CameraPage(CameraVM viewModel)
	{
        try{
            InitializeComponent();
            BindingContext = viewModel;
   
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    
	}

    // private void BackButton_Clicked(object sender, EventArgs e)
    // {
        
    // }

    // private void FlashButton_Clicked_1(object sender, EventArgs e)
    // {
    //     cameraReaderView.IsTorchOn = !cameraReaderView.IsTorchOn;
    // }
    // private int Capture = 0;
    // private async void OnCaptureClicked(object sender, EventArgs e)
    // {
    //     // var image = await cameraReaderView.CaptureAsync();
    //     // if (image != null)
    //     // {
    //     //         if (Capture == 0)
    //     //         {
    //     //             Capture = 1;
    //     //             var stream = await image.OpenReadAsync();
    //     //             // Save document to file or upload to server
    //     //             document.IsVisible = false;
    //     //             selfi.IsVisible = true;
    //     //             Label2.Text = "Please make sure you are taking photo in a well lighted area and  face in the circular frame .";
    //     //             Label1.Text = "Take a selfie";
    //     //         }
    //     //         else
    //     //         {
    //     //             Capture = 0;
    //     //             var stream = await image.OpenReadAsync();
    //     //             // Save photo to file or upload to server
    //     //             document.IsVisible = true;
    //     //             selfi.IsVisible = false;
    //     //             await Navigation.PushModalAsync(new NavigationPage(new ProccessingVerficationPage(new ViewModels.Verifyidentity.ProccessingVerficationVM())));
    //     //         }

    //     //     }
    // }

    // private void photo_Clicked(object sender, EventArgs e)
    // {

    // }
}