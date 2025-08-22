namespace NellsPay.Send.Services
{
    public class ToastService : IToastService
    {
        public void ShowToast(string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CommunityToolkit.Maui.Alerts.Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            });
        }
    }
}