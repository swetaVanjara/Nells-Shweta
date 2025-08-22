using CommunityToolkit.Maui.Views;
using NellsPay.Send.ViewModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PopUpPages;

namespace NellsPay.Send.Views;

public partial class PaymentWebview : ContentPage
{
	public PaymentWebview(PaymentWebViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		MyWebView.Navigating += OnWebViewNavigating;
	}

	private void OnWebViewNavigating(object? sender, WebNavigatingEventArgs e)
	{
		var currentUrl = e.Url;
		if (Uri.TryCreate(currentUrl, UriKind.Absolute, out var uri) &&
			uri.Host.EndsWith("nellspay.com", StringComparison.OrdinalIgnoreCase))
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				if (BindingContext is PaymentWebViewModel vm)
				{
					await vm.GetPaymentStatus();
				}
			});
		}
	}
}