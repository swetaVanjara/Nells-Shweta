using System;
using System.Text.Json.Serialization;
namespace NellsPay.Send.Contracts
{
    public class LoginRequestWrapper
    {
        [JsonPropertyName("loginRequestDto")]
        public LoginRequest LoginRequestDto { get; set; }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

