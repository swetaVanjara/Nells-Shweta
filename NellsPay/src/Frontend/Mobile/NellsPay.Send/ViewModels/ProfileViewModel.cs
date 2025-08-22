using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Messages;
using NellsPay.Send.ViewModels.LoginViewModels;
using NellsPay.Send.Views.LoginPages;
using NellsPay.Send.Views.PaymentSettingsPages;
using NellsPay.Send.Views.PopUpPages;
using NellsPay.Send.Views.ProfilePages;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels
{
    [QueryProperty(nameof(SavedI), "SavedItem")]
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILogoutService _logoutService;
        [ObservableProperty] private UserModel user = new();
        [ObservableProperty] private UserModel savedI = new();
        [ObservableProperty] private bool notification = true;
        [ObservableProperty] private string userInitials;

        partial void OnSavedIChanged(UserModel value)
        {
            User = value;
        }

        public ProfileViewModel(ISettingsProvider settingsProvider, ILogoutService logoutService)
        {
            _settingsProvider = settingsProvider;
            _logoutService = logoutService;
            User = new UserModel
            {
                UserFirstName = _settingsProvider.FullName,
                UserLastName = _settingsProvider.UserName,
                PhoneNumber = _settingsProvider.PhoneNumber,
                UserName = _settingsProvider.UserName,
                UserImage = "usertest.png",
                Address = _settingsProvider.AddressLine,
                BirthDate = new DateOnly(1997, 3, 22),
                Email = _settingsProvider.Email
            };
            UserInitials = $"{_settingsProvider.FullName?.FirstOrDefault()}{_settingsProvider.UserName?.FirstOrDefault()}".ToUpper();
            Notification = true;
        }

        #region RelayCommands
        [RelayCommand]
        private async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                await _logoutService.LogoutAsync();
            }
        }

        [RelayCommand]
        private async Task EditProfile()
        {
            await Shell.Current.GoToAsync($"{nameof(EditProfilePage)}?",
               new Dictionary<string, object>
               {
                   ["UserEdit"] = User,
               });   
        }

        [RelayCommand]
        private async Task PaymentSettings() => await Shell.Current.GoToAsync("//ProfilePage/PaymentSettingsPage");

        [RelayCommand]
        private async Task ChangePassword() => await Shell.Current.GoToAsync("//ProfilePage/ChangePasswordPage");

        [RelayCommand]
        private async Task TermsConditions()
        {
            
#if ANDROID
            await OpenFileFromPackage("terms_and_condition.pdf", "Terms & Conditions");
#else
            await Shell.Current.GoToAsync($"//ProfilePage/TermsOrPolicyPage?IsTerm=true");
#endif
        }

        [RelayCommand]
        private async Task PrivacyPolicy()
        {
            
#if ANDROID
            await OpenFileFromPackage("privacy_policy.pdf", "Privacy Policy");
#else
            await Shell.Current.GoToAsync($"//ProfilePage/TermsOrPolicyPage?IsTerm=false");
#endif
        }

        #endregion

        #region Command

        private async Task OpenFileFromPackage(string fileName, string title)
        {
            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                var localPath = Path.Combine(FileSystem.CacheDirectory, fileName);

                using (var outputStream = File.Create(localPath))
                {
                    await stream.CopyToAsync(outputStream);
                }

                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    Title = title,
                    File = new ReadOnlyFile(localPath)
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to open file: {ex.Message}", "OK");
            }
        }
        public ICommand FaqsCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//ProfilePage/FaqsPage");
        });
        // public ICommand LogoutCommand => new Command(() =>
        // {
        //     Shell.Current.CurrentPage.ShowPopup(new LogOutPopUp());
        // });
        public ICommand CameraCommsnd => new Command(() =>
        {

        });


        #endregion
    }
}
