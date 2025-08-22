using NellsPay.Send.ViewModels.LoginViewModels;

namespace NellsPay.Send.Views.LoginPages;

public partial class PINPage : ContentPage
{
    private Entry[] pinEntries;
    private Entry[] confirmPinEntries;
    public PINPage(PINVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        pinEntries = new[] { Pin1, Pin2, Pin3, Pin4 };
        confirmPinEntries = new[] { ConfirmPin1, ConfirmPin2, ConfirmPin3, ConfirmPin4 };

        foreach (var entry in pinEntries.Concat(confirmPinEntries))
        {
            entry.Focused += (s, e) => entry.Unfocus(); 
        }
    }
    private void NumberClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var availableEntry = pinEntries.Concat(confirmPinEntries).FirstOrDefault(e => string.IsNullOrEmpty(e.Text));
        if (availableEntry != null)
        {
            availableEntry.Text = button.Text;
        }
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        var lastFilledEntry = pinEntries.Concat(confirmPinEntries).LastOrDefault(e => !string.IsNullOrEmpty(e.Text));
        if (lastFilledEntry != null)
        {
            lastFilledEntry.Text = "";
        }
    }

    private void PinEntryChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null || string.IsNullOrEmpty(entry.Text)) return;

        var entries = pinEntries.Contains(entry) ? pinEntries : confirmPinEntries;
        var nextIndex = Array.IndexOf(entries, entry) + 1;

        if (nextIndex < entries.Length)
        {
            entries[nextIndex].Focus();
        }
        else
        {
            entry.Unfocus();
            App.Current.MainPage = new AppShell();
        }
    }

    private async void BiometricClicked(object sender, EventArgs e)
    {
        // Implement biometric authentication if needed
        await DisplayAlert("Biometric", "Fingerprint authentication triggered.", "OK");
    }

    private async void back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}