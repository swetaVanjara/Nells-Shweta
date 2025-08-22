using NellsPay.Send.ResponseModels;

namespace NellsPay.Send;

public interface ISignInService
{
    Task<(bool IsSuccess, TokenResultModel? Response)> AppleSignInAsync();
    Task<(bool IsSuccess, TokenResultModel? Response)> GoogleSignInAsync();

}