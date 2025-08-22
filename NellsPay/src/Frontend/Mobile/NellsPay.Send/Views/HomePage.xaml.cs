using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Helpers;

namespace NellsPay.Send.Views;

public partial class HomePage : ContentPage
{
    HomeViewModel _viewModel;
    public static HomePage Instance { get; private set; }
    private bool _isMessengerRegistered = false;

    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        Instance = this;
    }
    protected override async void OnAppearing()
    {
        _viewModel.updateIdentity();
        RegisterMessenger();
        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Color.FromArgb("#0A3269"),
            StatusBarStyle = StatusBarStyle.LightContent
        });
    }

    private void RegisterMessenger()
    {
        if (_isMessengerRegistered)
            return;

        WeakReferenceMessenger.Default.Register<object, string>(
            this,
            HomePageRefreshMessage.VerifiedData,
            async (recipient, token) => await RefreshVerificationData()
        );

        WeakReferenceMessenger.Default.Register<object, string>(
            this,
            HomePageRefreshMessage.UpdateData,
            async (recipient, token) => await UpdateData()
        );

        _isMessengerRegistered = true;
    }

    private async Task UpdateData()
    {
       if (BindingContext is HomeViewModel vm)
        {
            await vm.GetData();
        }
    }

    public async Task RefreshVerificationData()
    {
        if (BindingContext is HomeViewModel vm)
        {
            await vm.GetData();
            vm.VerifyIdVisible = false;
            vm.SendMoneyVisible = true;
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (AppInfo.RequestedTheme == AppTheme.Dark)
        {
            this.Behaviors.Add(new StatusBarBehavior
            {
                StatusBarColor = Colors.Black,
                StatusBarStyle = StatusBarStyle.LightContent
            });
        }
        else
        {
            this.Behaviors.Add(new StatusBarBehavior
            {
                StatusBarColor = Colors.White,
                StatusBarStyle = StatusBarStyle.DarkContent
            });
        }
    }
}