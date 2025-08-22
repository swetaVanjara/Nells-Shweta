using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface IUserApi
    {
        [Post("/users/register")]
        Task<RegisterResponse?> Register([Body] RegisterRequestWrapper registerRequest);

        [Post("/users/login")]
        Task<LoginResponseResponseWrapper?> Login([Body] LoginRequestWrapper registerRequest);

        [Post("/users/refresh-token")]
        Task<LoginResponseResponseWrapper?> RefreshToken([Body] RefreshTokenRequestWrapper registerRequest);

    }
}

