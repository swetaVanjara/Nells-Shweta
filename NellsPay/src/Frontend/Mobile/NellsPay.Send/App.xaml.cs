using NellsPay.Send.ViewModels.LoginViewModels;
using NellsPay.Send.ViewModels.Verifyidentity;
using NellsPay.Send.Views.LoginPages;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.RecipientsPages;
using NellsPay.Send.Views.Verifyidentity;

namespace NellsPay.Send
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Services?.GetService<ITokenRefreshService>()?.Stop();
        }

        public App(IServiceProvider serviceProvider)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcyMjk0MkAzMjM4MmUzMDJlMzBORmw1K3RYSEVwU3g4TVJoSHFTdEF6ZGY0bnVSQnEzWkhGRmNpeHNZZGtzPQ==;MzcyMjk0M0AzMjM4MmUzMDJlMzBlbDVpS2tFa0FvUitRMUl1VTJoMUhmbVRxSjVUWWxTU1lEc2FDeVlENEhRPQ==");


            InitializeComponent();

            // tokenRefreshService.Start();
            Services = serviceProvider;
            if (Application.Current?.Resources?.MergedDictionaries != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

                if (Application.Current?.RequestedTheme != null)
                {
                    AppTheme currentTheme = Application.Current.RequestedTheme;
                    var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();

                    if (theme != null)
                    {
                        if (currentTheme is AppTheme.Dark)
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                            Application.Current.UserAppTheme = AppTheme.Dark;
                        }
                        else
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                            Application.Current.UserAppTheme = AppTheme.Light;
                        }
                    }
                }
            }

            EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, entry) =>
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            SearchBarHandler.Mapper.AppendToMapping(nameof(SearchBar), (handler, searchBar) =>
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                //handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // to check user login and pin pages
            var settings = Services?.GetService<ISettingsProvider>();

            var email = settings?.Email;

            Page startPage;
            startPage = new NavigationPage(new OnboardingPage());

            return new Window(startPage);
        }
    }
}