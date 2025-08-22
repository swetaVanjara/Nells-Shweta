using Foundation;
using UIKit;

namespace NellsPay.Send;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
		return WebAuthenticator.Default.OpenUrl(application, url, options);
    }
}
