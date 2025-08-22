using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.Repository;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;
using NellsPay.Send.ViewModels.LoginViewModels;
using NellsPay.Send.Views.LoginPages;

namespace NellsPay.Send.Services.Contracts
{
    public class LogoutService : ILogoutService
    {
        private readonly IDbContext _db;

        public LogoutService(IDbContext db) => _db = db;

        public async Task LogoutAsync()
        {
            if (_db is MobileDbContext mobile)
            {
                await mobile.DropAllTablesAsync();
            }

            Preferences.Default.Clear();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var loginVM = App.Services!.GetRequiredService<LoginVM>();
                Application.Current.MainPage = new NavigationPage(new LoginPage(loginVM));
            });
        }
    }

}