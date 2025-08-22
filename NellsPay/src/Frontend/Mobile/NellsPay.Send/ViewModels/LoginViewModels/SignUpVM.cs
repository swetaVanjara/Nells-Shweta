using CommunityToolkit.Maui.Views;
using NellsPay.Send.Contracts;
using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Navigation;
using NellsPay.Send.Views.LoginPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.LoginViewModels
{
    public class SignUpVM : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly ISettingsProvider _settingProvider;
        private readonly ISignUpValidationService _validationService;

        private readonly ISignInService _signInService;

        private readonly INavigationService _navigationService;
        private readonly IToastService _toastService;

        #region Fields
        private SignUpModel _SignUpM { get; set; } = new SignUpModel();
        #endregion
        #region Property
        public SignUpModel SignUpM
        {
            get => _SignUpM;
            set
            {
                _SignUpM = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AsyncRelayCommand SignUpCommand { get; private set; }
        public AsyncRelayCommand GoogleCommand { get; private set; }
        public AsyncRelayCommand AppleCommand { get; private set; }
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public SignUpVM(IUserService userService, ISettingsProvider settingsProvider, INavigationService navigationService, IToastService toastService, ISignInService signInService, ISignUpValidationService validationService)
        {
            try
            {
                _userService = userService;
                _navigationService = navigationService;
                _settingProvider = settingsProvider;
                _toastService = toastService;
                _signInService = signInService;
                _validationService = validationService;
                SignUpCommand = new AsyncRelayCommand(SignUp);
                AppleCommand = new AsyncRelayCommand(SignUpWithApple);
                GoogleCommand = new AsyncRelayCommand(SignUpWithGogole);
            }
            catch (Exception ex)
            {

            }

        }

        private async Task SignUpWithApple()
        {
            var result = await _signInService.AppleSignInAsync();

            if (result.IsSuccess)
            {
                // string? email = result.Email;
                // string? fullName = result.FullName;

                // _toastService.ShowToast($"Welcome {email ?? fullName ?? "user"}!");
            }
            else
            {
                _toastService.ShowToast("Apple sign-in failed or was cancelled.");
            }

        }

        private async Task SignUpWithGogole()
        {
            var userInfo = await _signInService.GoogleSignInAsync();

            if (userInfo.IsSuccess)
            {
                _settingProvider.AccessToken = userInfo.Response.AccessToken ?? "";
                _settingProvider.FullName = userInfo.Response.UserInfo.family_name ?? "";
                _settingProvider.UserName = userInfo.Response.UserInfo.given_name ?? "";
                _settingProvider.RefreshToken = userInfo.Response.RefreshToken ?? "";
                _settingProvider.UserId = userInfo.Response.UserInfo.sid;
                var issuedAt = DateTime.Now;
                _settingProvider.LastUpdatedRefreshTokenTime = issuedAt.ToString("o");
                _settingProvider.Email = userInfo.Response.UserInfo.email;
                App.Current.MainPage = new AppShell();
            }
            else
            {
                _toastService.ShowToast("Google sign-in failed or was cancelled.");
            }
        }

        private async Task SignUp()
        {
            try
            {
                _validationService.ClearStatuses(SignUpM);

                if (!_validationService.Validate(SignUpM, _toastService))
                    return;

                var payload = new RegisterRequestWrapper
                {
                    UserRegistrationDto = new RegisterRequest
                    {
                        Email = SignUpM.Email,
                        FirstName = SignUpM.FirstName,
                        LastName = SignUpM.LastName,
                        Password = SignUpM.Password
                    }
                };
                IsLoading = true;
                var response = await _userService.Register(payload);

                if (string.IsNullOrEmpty(response?.UserId))
                {
                    _toastService.ShowToast("Registration failed. Please try again.");
                }
                else
                {
                    _toastService.ShowToast("Registration successful!");
                    await _navigationService.PushAsync<LoginPage>();
                }
            }
            catch
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        public ICommand TermCommand => new Command(() =>
        {
            //
        });
        public ICommand PrivacyCommand => new Command(() =>
        {
            //
        });
        public ICommand LoginCommand => new Command(async () =>
        {
            var vm = App.Services?.GetService<LoginVM>();

            App.Current.MainPage = new NavigationPage(new LoginPage(vm));
        });

    }
}
