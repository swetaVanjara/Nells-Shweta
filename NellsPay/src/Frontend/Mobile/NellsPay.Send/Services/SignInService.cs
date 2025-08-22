using System;
using NellsPay.Send.Helpers;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services;

public class SignInService : ISignInService
{
    private readonly IToastService _toastService;

    public SignInService(IToastService toastService)
    {
        _toastService = toastService;
    }


    public async Task<(bool IsSuccess, TokenResultModel? Response)> AppleSignInAsync()
    {
        string clientId = "nellspay-mobile";
        string redirectUri = "nellspay://auth/callback";

        string loginUrl = $"https://identity.nellspay.com/realms/nellspay/protocol/openid-connect/auth" +
                          $"?client_id={clientId}" +
                          $"&redirect_uri={redirectUri}" +
                          $"&response_type=code" +
                          $"&scope=openid email profile" +
                          $"&kc_idp_hint=apple" + $"&prompt=login";

        try
        {
            WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
                new WebAuthenticatorOptions
                {
                    Url = new Uri(loginUrl),
                    CallbackUrl = new Uri(redirectUri),
                    PrefersEphemeralWebBrowserSession = true
                });

            if (result?.Properties.TryGetValue("code", out var code) == true)
            {
                var userInfo = await ExchangeCodeForTokenAsync(code, clientId, redirectUri);
                if (userInfo != null)
                {
                    return (true, userInfo);
                }
            }
        }
        catch (Exception)
        {
            _toastService.ShowToast("Apple Sign-In failed.");
        }

        return (false, null);
    }

    public async Task<(bool IsSuccess, TokenResultModel? Response)> GoogleSignInAsync()
    {
        string clientId = "nellspay-mobile";
        string redirectUri = "nellspay://auth/callback";

        string loginUrl = $"https://identity.nellspay.com/realms/nellspay/protocol/openid-connect/auth" +
                          $"?client_id={clientId}" +
                          $"&redirect_uri={redirectUri}" +
                          $"&response_type=code" +
                          $"&scope=openid email profile" +
                          $"&kc_idp_hint=google" + $"&prompt=login";
        Console.WriteLine($"Login URL: {loginUrl}");

        try
        {
            WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
                new WebAuthenticatorOptions
                {
                    Url = new Uri(loginUrl),
                    CallbackUrl = new Uri(redirectUri),
                    PrefersEphemeralWebBrowserSession = true
                });

            if (result?.Properties.TryGetValue("code", out var code) == true)
            {
                var userInfo = await ExchangeCodeForTokenAsync(code, clientId, redirectUri);
                if (userInfo != null)
                {
                    return (true, userInfo);
                }
            }
        }
        catch (Exception)
        {
            _toastService.ShowToast("Google Sign-In failed.");
        }

        return (false, null);
    }

    private async Task<TokenResultModel?> ExchangeCodeForTokenAsync(string code, string clientId, string redirectUri)
    {
        string tokenEndpoint = "https://identity.nellspay.com/realms/nellspay/protocol/openid-connect/token";

        using var client = new HttpClient();
        var parameters = new Dictionary<string, string>
        {
            { "client_id", "nellspay-mobile" },
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", "nellspay://auth/callback" }
        };


        var content = new FormUrlEncodedContent(parameters);
        var response = await client.PostAsync(tokenEndpoint, content);
        var contentType = response.Content.Headers.ContentType?.ToString();
        var rawBytes = await response.Content.ReadAsByteArrayAsync();

        var rawString = System.Text.Encoding.UTF8.GetString(rawBytes);
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Content-Type: {contentType}");
        Console.WriteLine($"Raw Response: {rawString}");

        var json = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var tokenResult = JsonSerializer.Deserialize<NellsPay.Send.ResponseModels.AuthTokenResponse>(json);
            if (!string.IsNullOrEmpty(tokenResult?.access_token))
            {
                var userInfo = JwtHelper.DecodePayload<JwtPayloadModel>(tokenResult.access_token);
                return new TokenResultModel
                {
                    AccessToken = tokenResult.access_token,
                    RefreshToken = tokenResult.refresh_token,
                    ExpiresIn = tokenResult.expires_in,
                    RefreshExpiresIn = tokenResult.refresh_expires_in,
                    UserInfo = userInfo
                };
            }
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Token Error", json, "OK");
        }

        return null;
    }
}