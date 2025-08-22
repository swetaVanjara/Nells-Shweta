using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace NellsPay.Send.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Color.FromArgb("#0A3269"),
            StatusBarStyle = StatusBarStyle.LightContent
        });
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
      
    
    private void BackButton_Clicked(object sender, EventArgs e)
    {

    }
}