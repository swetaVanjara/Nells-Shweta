using System;
using System.Net;
using NellsPay.Send.Contracts;
using NellsPay.Send.Exceptions;
using NellsPay.Send.Helpers;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;
using NellsPay.Send.Views.LoginPages;
using Refit;

namespace NellsPay.Send.Services
{
    public class UserService(ISettingsProvider settingsProvider) : BaseService, IUserService
    {
        private readonly IUserApi _userApi = HttpClientProvider.Instance.GetApi<IUserApi>();


        public async Task<LoginResponseResponseWrapper?> Login(LoginRequestWrapper request)
        {
            try
            {
                return await _userApi.Login(request);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool?> RefreshToken()
        {
            try
            {
                if (!DateTime.TryParse(settingsProvider.LastUpdatedRefreshTokenTime, out var lastIssuedTime))
                    return await HandleTokenFailure();

                TimeSpan timeSinceIssued = DateTime.Now - lastIssuedTime;

                if (timeSinceIssued.TotalMinutes >= 10.0)
                {
                    var request = new RefreshTokenRequestWrapper()
                    {
                        RefreshToken = new RefreshTokenRequest()
                        {
                            RefreshToken = settingsProvider.RefreshToken
                        }
                    };

                    var response = await _userApi.RefreshToken(request);
                    if (response?.TokenResponse != null)
                    {
                        settingsProvider.AccessToken = response.TokenResponse.AccessToken ?? "";
                        settingsProvider.RefreshToken = response.TokenResponse.RefreshToken ?? "";
                        settingsProvider.LastUpdatedRefreshTokenTime = DateTime.Now.ToString("o");
                        return true;
                    }

                    return await HandleTokenFailure();
                }

                return true;
            }
            catch
            {
                return await HandleTokenFailure();
            }
        }


        public async Task<bool?> CheckSessionIsValidOrNot()
        {
            try
            {
                var request = new RefreshTokenRequestWrapper()
                {
                    RefreshToken = new RefreshTokenRequest()
                    {
                        RefreshToken = settingsProvider.RefreshToken
                    }
                };

                var response = await _userApi.RefreshToken(request);
                if (response?.TokenResponse != null) {
                    settingsProvider.AccessToken = response.TokenResponse.AccessToken ?? "";
                    settingsProvider.RefreshToken = response.TokenResponse.RefreshToken ?? "";
                    settingsProvider.LastUpdatedRefreshTokenTime = DateTime.Now.ToString("o");
                    return true;
                }
                else
                {
                    ClearData();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool?> HandleTokenFailure()
        {
            // Clear credentials
            ClearData();

            // Delay slightly to ensure main thread is available
            await Task.Delay(100);

            // Redirect to login
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new NavigationPage(new OnboardingPage());
            });

            return false;
        }

        private void ClearData()
        {
            Preferences.Default.Clear();
             
            settingsProvider.AccessToken = string.Empty;
            settingsProvider.RefreshToken = string.Empty;
            settingsProvider.Email = string.Empty;
            settingsProvider.LastUpdatedRefreshTokenTime = string.Empty;
             
        }

        async Task<RegisterResponse?> IUserService.Register(RegisterRequestWrapper request)
        {
            try
            {
                var response = await _userApi.Register(request);
                await Task.Delay(5000);
                return response;
            }
            catch
            {
                return null;
            }
        }

    }
}

