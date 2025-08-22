using NellsPay.Send.ViewModels.LoginViewModels;
using System.Threading.Tasks;

namespace NellsPay.Send.Views.LoginPages;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(3000);

        // wait for 3 seconds
        var userService = App.Services!.GetRequiredService<IUserService>();

        var settingsProvider = App.Services!.GetRequiredService<ISettingsProvider>();
        var loginVM = App.Services!.GetRequiredService<LoginVM>();


        if (string.IsNullOrWhiteSpace(settingsProvider.Email))
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage(loginVM));
        }
        else
        {
            if (!DateTime.TryParse(settingsProvider.LastUpdatedRefreshTokenTime,
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None,
                          out var lastIssuedTime))
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage(loginVM));
                return;
            }

            TimeSpan timeSinceIssued = DateTime.Now - lastIssuedTime;

            if (timeSinceIssued.TotalMinutes >= 10.0)
            {
                var isTokenValid = await userService?.CheckSessionIsValidOrNot()!;
                if (isTokenValid ?? false)
                {
                    App.Current.MainPage = new AppShell();
                    return;
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage(loginVM));
                    return;
                }

            }

            App.Current.MainPage = new AppShell();
        }


    }


    private async void SignUp_Clicked(object sender, EventArgs e)
    {
        // await Navigation.PushAsync(new SignUpPage(new SignUpVM()));
    }

    private async void Login_Clicked_1(object sender, EventArgs e)
    {
        // await Navigation.PushAsync(new LoginPage(new LoginVM()));
    }
}