using Microsoft.Maui.Handlers;
using WebKit;
using NellsPay.Send.Helpers;
using NellsPay.Send.Views.MoneyTransferFlowPages;

namespace NellsPay.Send.Platforms.iOS.Handlers
{
    public class CustomWebViewHandler : WebViewHandler
    {
        protected override void ConnectHandler(WKWebView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.NavigationDelegate = new CustomNavigationDelegate();
            platformView.UIDelegate = new CustomUIDelegate();
        }
    }

    public class CustomNavigationDelegate : WKNavigationDelegate
    {
        public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            var url = navigationAction.Request.Url?.AbsoluteString ?? "";
            var scheme = navigationAction.Request.Url?.Scheme ?? "";
            var host = navigationAction.Request.Url?.Host ?? "";

            Console.WriteLine($"Intercepted URL: {url}");

            if (!scheme.Equals("http", StringComparison.OrdinalIgnoreCase) &&
                !scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Blocked non-HTTP(s) scheme: " + scheme);
                decisionHandler(WKNavigationActionPolicy.Cancel);
                return;
            }

            if (!string.IsNullOrEmpty(host) && host.EndsWith("nellspay.com", StringComparison.OrdinalIgnoreCase))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        // Get the current page in Shell
                        if (Shell.Current.CurrentPage is PaymentWebview page &&
                            page.BindingContext is PaymentWebViewModel vm)
                        {
                            await vm.GetPaymentStatus(); 
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Redirect failed: " + ex.Message);
                    }
                });

                decisionHandler(WKNavigationActionPolicy.Cancel);
                return;
            }

            const WKNavigationActionPolicy AllowWithoutAppLink = (WKNavigationActionPolicy)((int)WKNavigationActionPolicy.Allow + 2);
            decisionHandler(AllowWithoutAppLink);
        }

        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
           
        }


    }

    public class CustomUIDelegate : WKUIDelegate
    {
        public override WKWebView CreateWebView(WKWebView webView,
         WKWebViewConfiguration configuration,
         WKNavigationAction navigationAction,
         WKWindowFeatures windowFeatures)
        {

            if (navigationAction.TargetFrame == null || !navigationAction.TargetFrame.MainFrame)
            {
                webView.LoadRequest(navigationAction.Request);
            }

            return null;
        }
    }
}