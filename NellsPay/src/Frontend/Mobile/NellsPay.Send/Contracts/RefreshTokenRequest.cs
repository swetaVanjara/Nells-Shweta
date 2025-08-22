using System;
using System.Text.Json.Serialization;
namespace NellsPay.Send.Contracts
{
    public class RefreshTokenRequestWrapper
    {
        [JsonPropertyName("refreshToken")]
        public RefreshTokenRequest RefreshToken { get; set; }
    }
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}

