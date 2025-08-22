using System;
namespace NellsPay.Send.ResponseModels
{
    public class TokenResultModel
    {
         public string? AccessToken { get; set; }
         public string? RefreshToken { get; set; }
         public int ExpiresIn { get; set; }
         public int RefreshExpiresIn { get; set; }
         public JwtPayloadModel? UserInfo { get; set; }
    }
}

