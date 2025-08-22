using NellsPay.Send.Contracts;
using NellsPay.Send.Helpers;
using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Navigation;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.Views.LoginPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.LoginViewModels
{
    public class LoginVM : BaseViewModel
    {
        private readonly ISignInService _signInService;
        private readonly IUserService _userService;
        private readonly IKycService _kycService;
        private readonly ICustomerService _customerService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly INavigationService _navigationService;
        private readonly IToastService _toastService;
        private LoginModel _Login { get; set; } = new LoginModel();
        public AsyncRelayCommand LoginCommand { get; private set; }
        public AsyncRelayCommand GoogleCommand { get; private set; }
        public AsyncRelayCommand AppleCommand { get; private set; }
        public AsyncRelayCommand SignUpCommand { get; private set; }
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
        public LoginModel Login
        {
            get => _Login;
            set
            {
                _Login = value;
                OnPropertyChanged();
            }
        }

        public LoginVM(IUserService userService, ICustomerService customerService, IKycService kycService, ISignInService signInService, ISettingsProvider settingsProvider, INavigationService navigationService, IToastService toastService)
        {
            _kycService = kycService;
            _signInService = signInService;
            _userService = userService;
            _navigationService = navigationService;
            _settingsProvider = settingsProvider;
            _customerService = customerService;
            _toastService = toastService;
            LoginCommand = new AsyncRelayCommand(ManualLogin);
            GoogleCommand = new AsyncRelayCommand(LoginWithGoogle);
            AppleCommand = new AsyncRelayCommand(LoginWithApple);
            SignUpCommand = new AsyncRelayCommand(RedirectToSignUp);
        }

        private async Task LoginWithApple()
        {
            var result = await _signInService.AppleSignInAsync();
            if (result.IsSuccess && !string.IsNullOrEmpty(result.Response.AccessToken))
            {
                await HandleLoginFromToken(result.Response.AccessToken, result.Response.RefreshToken);
            }
            else
            {
                _toastService.ShowToast("Apple sign-in failed or was cancelled.");
            }
        }



        private async Task RedirectToSignUp()
        {
            try
            {
                var vm = App.Services?.GetService<SignUpVM>();

                App.Current.MainPage = new NavigationPage(new SignUpPage(vm));
            }
            catch (Exception)
            {

            }
        }
        private async Task ManualLogin()
        {
            try
            {
                var payload = new LoginRequestWrapper
                {
                    LoginRequestDto = new LoginRequest
                    {
                        Email = Login.Email,

                        Password = Login.Password
                    }
                };
                IsLoading = true;
                var response = await _userService.Login(payload);
                var accessToken = response?.TokenResponse?.AccessToken;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    await HandleLoginFromToken(accessToken, response?.TokenResponse?.RefreshToken);
                }
                else
                {
                    _toastService.ShowToast("Login failed. Please try again.");
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

        private async Task HandleLoginFromToken(string accessToken, string? refreshToken)
        {
            var userInfo = JwtHelper.DecodePayload<JwtPayloadModel>(accessToken);
            if (userInfo == null)
            {
                _toastService.ShowToast("Failed to decode token.");
                return;
            }

            _settingsProvider.AccessToken = accessToken;
            _settingsProvider.FullName = userInfo.family_name;
            _settingsProvider.UserName = userInfo.given_name;
            _settingsProvider.Email = userInfo.email;
            _settingsProvider.RefreshToken = refreshToken ?? string.Empty;
            _settingsProvider.LastUpdatedRefreshTokenTime = DateTime.Now.ToString("o");

            var customer = await _customerService.GetCustomerByEmail(userInfo.email);
            if (customer?.Customer == null)
            {
                _toastService.ShowToast($"Failed to fetch customer data for {userInfo.email}");
                return;
            }

            _settingsProvider.CustomerId = customer.Customer.Id.ToString();
            _settingsProvider.UserId = customer.Customer.UserId.ToString();
            _settingsProvider.DateOfBirth = customer.Customer.DateOfBirth;
            _settingsProvider.LastName = customer.Customer.LastName;
            _settingsProvider.country2Code = customer.Customer.country2Code;
            _settingsProvider.AddressLine = customer.Customer.AddressLine1;
            _settingsProvider.CitizenshipCode = customer.Customer.country2Code;
            _settingsProvider.Country = customer.Customer.Country;
            _settingsProvider.Gender = customer.Customer.Gender;
            _settingsProvider.Region = customer.Customer.Region;
            _settingsProvider.PostCode = customer.Customer.PostCode;
            _settingsProvider.City = customer.Customer.City;
            _settingsProvider.PhoneNumber = customer.Customer.PhoneNumber;



            var profile = await _kycService.GetProfilebyId(_settingsProvider.CustomerId);
            if (profile?.Profile != null)
            {
                _settingsProvider.ProfileId = profile.Profile.Id.ToString();
            }
            else
            {
                _toastService.ShowToast("Profile ID is null.");
            }

            App.Current.MainPage = new AppShell();
            _toastService.ShowToast($"Welcome {userInfo.name} ({userInfo.email})");
        }

        private async Task LoginWithGoogle()
        {
            var result = await _signInService.GoogleSignInAsync();

            if (result.IsSuccess && !string.IsNullOrEmpty(result.Response.AccessToken))
            {
                await HandleLoginFromToken(result.Response.AccessToken, result.Response.RefreshToken);
            }
            else
            {
                _toastService.ShowToast("Google sign-in failed or was cancelled.");
            }
        }

        public ICommand ForgetPasswordCommand => new Command(() =>
        {
            //
        });

        public ICommand TermCommand => new Command(() =>
        {
            //
        });
        public ICommand PrivacyCommand => new Command(() =>
        {
            //
        });

    }
}
