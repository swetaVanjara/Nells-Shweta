using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using static Android.Views.View;

namespace NellsPay.Send;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Window.DecorView.SetOnApplyWindowInsetsListener(new MyInsetsListener());
    }
}

public class MyInsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
{

    public WindowInsets OnApplyWindowInsets(Android.Views.View v, WindowInsets insets)
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.VanillaIceCream) // API 35+
        {
            var topInset = insets.GetInsetsIgnoringVisibility(WindowInsets.Type.StatusBars()).Top;
            v.SetPadding(0, topInset, 0, 0);
        }

        return insets;
    }
}


