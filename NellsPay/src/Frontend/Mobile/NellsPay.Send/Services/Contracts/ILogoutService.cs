using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services.Contracts
{
    public interface ILogoutService
    {
        Task LogoutAsync();
    }
    
}