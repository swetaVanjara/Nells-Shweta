using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;

namespace NellsPay;

[Activity(NoHistory = true, Exported = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(new[] { Android.Content.Intent.ActionView }, Categories = new[] {  Android.Content.Intent.CategoryDefault,
        Android.Content.Intent.CategoryBrowsable },
    DataScheme = "nellspay",
    DataHost = "auth",
    DataPath = "/callback"  )]
public class WebAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Log.Debug("WebAuthCallback", "Callback received");
    }

    protected override void OnResume()
    {
        base.OnResume();        
    }
}