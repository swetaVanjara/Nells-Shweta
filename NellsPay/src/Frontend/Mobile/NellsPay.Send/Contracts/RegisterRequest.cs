using System;
using System.Text.Json.Serialization;
namespace NellsPay.Send.Contracts
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
    
    public class RegisterRequestWrapper
    {
        [JsonPropertyName("userRegistrationDto")]
        public RegisterRequest UserRegistrationDto { get; set; }
    }
}

