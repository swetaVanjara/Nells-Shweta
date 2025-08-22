using NellsPay.Send.Models.LoginModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface ISettingsProvider
    {
        string LastUpdatedRefreshTokenTime { get; set; }
        string RefreshToken { get; set; }
        string AccessToken { get; set; }
        public DateTime DateOfBirth { get; set; }
        string? country2Code { get; set; }
        string? LastName { get; set; }
        string? City { get; set; }
        string? Region { get; set; }
        string? PostCode { get; set; }
        string? Country { get; set; }
        string? Gender { get; set; }
        string? PhoneNumber { get; set; }
        string? AddressLine { get; set; }
        string? CitizenshipCode { get; set; }
        string UserName { get; set; }
        string FullName { get; set; }
        string Email { get; set; }
        string UserId { get; set; }
        string CustomerId { get; set; }
        string ProfileId { get; set; }
        bool Verifyidentity { get; set; }
        string Session { get; set; }
        string SelectedDocumentId { get; set; }
    }
}
