using NellsPay.Send.Constants;

namespace NellsPay.Send.Services;

public class SettingsProvider : ISettingsProvider
{
    public string RefreshToken
    {
        get => Preferences.Default.Get(Settings.RefreshTokenKey, string.Empty);
        set => Preferences.Default.Set(Settings.RefreshTokenKey, value);
    }

    public string UserName
    {
        get => Preferences.Default.Get(Settings.UsernameKey, string.Empty);
        set => Preferences.Default.Set(Settings.UsernameKey, value);
    }

    public string UserId
    {
        get => Preferences.Default.Get(Settings.UseridKey, string.Empty);
        set => Preferences.Default.Set(Settings.UseridKey, value);
    }

    public string Email
    {
        get => Preferences.Default.Get(Settings.EmailKey, string.Empty);
        set => Preferences.Default.Set(Settings.EmailKey, value);
    }

    public string Session
    {
        get => Preferences.Default.Get(Settings.SessionKey, string.Empty);
        set => Preferences.Default.Set(Settings.SessionKey, value);
    }

    public string SelectedDocumentId
    {
        get => Preferences.Default.Get(Settings.SelectedDocumentIdKey, string.Empty);
        set => Preferences.Default.Set(Settings.SelectedDocumentIdKey, value);
    }

    public string FullName
    {
        get => Preferences.Default.Get(Settings.FullnameKey, string.Empty);
        set => Preferences.Default.Set(Settings.FullnameKey, value);
    }
    public string LastUpdatedRefreshTokenTime
    {
        get => Preferences.Default.Get(Settings.LastUpdatedRefreshTokenTimeKey, string.Empty);
        set => Preferences.Default.Set(Settings.LastUpdatedRefreshTokenTimeKey, value);
    }

    public string AccessToken
    {
        get => Preferences.Default.Get(Settings.AccessTokenKey, string.Empty);
        set => Preferences.Default.Set(Settings.AccessTokenKey, value);
    }
    public string CustomerId
    {
        get => Preferences.Default.Get(Settings.CustomeridKey, string.Empty);
        set => Preferences.Default.Set(Settings.CustomeridKey, value);
    }
    public string ProfileId
    {
        get => Preferences.Default.Get(Settings.ProfileIdKey, string.Empty);
        set => Preferences.Default.Set(Settings.ProfileIdKey, value);
    }

    public bool Verifyidentity
    {
        get => Preferences.Default.Get(Settings.VerifyidentityKey, true);
        set => Preferences.Default.Set(Settings.VerifyidentityKey, value);
    }


    public DateTime DateOfBirth
    {
        get => Preferences.Default.Get(Settings.DateOfBirthKey, DateTime.MinValue);
        set => Preferences.Default.Set(Settings.DateOfBirthKey, value);
    }
    public string country2Code
    {
        get => Preferences.Default.Get(Settings.country2CodeKey, string.Empty);
        set => Preferences.Default.Set(Settings.country2CodeKey, value);
    }
    public string LastName
    {
        get => Preferences.Default.Get(Settings.LastNameKey, string.Empty);
        set => Preferences.Default.Set(Settings.LastNameKey, value);
    }
    public string AddressLine
    {
        get => Preferences.Default.Get(Settings.AddressLineKey, string.Empty);
        set => Preferences.Default.Set(Settings.AddressLineKey, value);
    }
    public string CitizenshipCode
    {
        get => Preferences.Default.Get(Settings.CitizenshipCodeKey, string.Empty);
        set => Preferences.Default.Set(Settings.CitizenshipCodeKey, value);
    }

    public string City
    {
        get => Preferences.Default.Get(Settings.CityKey, string.Empty);
        set => Preferences.Default.Set(Settings.CityKey, value);
    }


    public string Region
    {
        get => Preferences.Default.Get(Settings.RegionKey, string.Empty);
        set => Preferences.Default.Set(Settings.RegionKey, value);
    }


    public string PostCode
    {
        get => Preferences.Default.Get(Settings.PostCodeKey, string.Empty);
        set => Preferences.Default.Set(Settings.PostCodeKey, value);
    }

    public string Country
    {
        get => Preferences.Default.Get(Settings.CountryKey, string.Empty);
        set => Preferences.Default.Set(Settings.CountryKey, value);
    }

    public string Gender
    {
        get => Preferences.Default.Get(Settings.GenderKey, string.Empty);
        set => Preferences.Default.Set(Settings.GenderKey, value);
    }

    public string PhoneNumber
    {
        get => Preferences.Default.Get(Settings.PhoneNumberKey, string.Empty);
        set => Preferences.Default.Set(Settings.PhoneNumberKey, value);
    }

}