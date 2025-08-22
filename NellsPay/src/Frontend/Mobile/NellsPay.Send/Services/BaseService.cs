using System;
namespace NellsPay.Send.Services
{
    public class BaseService
    {
        protected string Auth => $"Bearer {Preferences.Get(NellsPay.Send.Constants.Settings.AccessTokenKey, "")}";

        protected bool IsInternetAvailable
        {
            get
            {
                var current = Connectivity.NetworkAccess;
                return current == NetworkAccess.Internet;
            }
        }
    }
}