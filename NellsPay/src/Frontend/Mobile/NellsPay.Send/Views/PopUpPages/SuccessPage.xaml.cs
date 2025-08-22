using CommunityToolkit.Maui.Views;

namespace NellsPay.Send.Views.PopUpPages;

public partial class SuccessPage : Popup
{

    public SuccessPage(string SuccesTitle, string Succesdescription, string buttonLabel, bool checkSuccessFailed)
    {
        InitializeComponent();

        PopUpDescription.Text = Succesdescription;

        Opened += async (s, e) => await AnimateOpen();

        OkButton.Text = buttonLabel;
        SuccessLogo.IsVisible = checkSuccessFailed;
        FailedLogo.IsVisible = !checkSuccessFailed;
    }


    private async Task AnimateOpen()
    {
        PopupContent.TranslationY = 300; // Start below
        await PopupContent.TranslateTo(0, 0, 300, Easing.CubicOut);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await AnimateClose();
    }

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Down)
        {
            await AnimateClose();
        }
       
    }
    private async Task AnimateClose()
    {
        await PopupContent.TranslateTo(0, 300, 300, Easing.CubicIn);
        Dispatcher.Dispatch(() => Close());
    }
}