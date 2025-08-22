using System;
namespace NellsPay.Send.ResponseModels
{
    public class LoginResponseResponseWrapper : ApiErrorResponse
    {
        public TokenResponse TokenResponse { get; set; }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
    }
}

