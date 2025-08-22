using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface IUserService
    {
        Task<LoginResponseResponseWrapper?> Login(LoginRequestWrapper request);
        Task<RegisterResponse?> Register(RegisterRequestWrapper request);
        Task<bool?> RefreshToken();
        Task<bool?> CheckSessionIsValidOrNot();
    }
}

