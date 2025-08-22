
namespace NellsPay.Send.Views.ProfilePages;

[QueryProperty(nameof(IsTerm), "IsTerm")]
public partial class TermsOrPolicyPage : ContentPage, INotifyPropertyChanged
{
    public bool IsTerm { get; set; }
    public TermsOrPolicyPage()
    {
        InitializeComponent();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (IsTerm)
            await LoadDocument("Terms & Conditions", "terms_and_condition", "pdf");
        else
            await LoadDocument("Privacy Policy", "privacy_policy", "pdf");
    }

    private async Task LoadDocument(string title, string fileName, string extension)
    {
        PageTitle.Text = title;
        var webView = new WebView();

#if ANDROID
        webView.Source = $"file:///android_asset/{fileName}.{extension}";
#elif IOS
        var filePath = Foundation.NSBundle.MainBundle.PathForResource(fileName, extension);
        webView.Source = $"file://{filePath}";
#endif

        scrollVw.Content = webView;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}