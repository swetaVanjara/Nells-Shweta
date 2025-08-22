using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.CustomControls
{
    public class ThemedContentPage : ContentPage
    {
        protected virtual bool UseTabStyle => false;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Clear previous behavior
            var existing = Behaviors.OfType<StatusBarBehavior>().FirstOrDefault();
            if (existing != null)
                Behaviors.Remove(existing);

            // Apply correct status bar behavior
            if (UseTabStyle)
            {
                // Custom tab page style
                Behaviors.Add(new StatusBarBehavior
                {
                    StatusBarColor = Color.FromArgb("#0A3269"),
                    StatusBarStyle = StatusBarStyle.DarkContent
                });
            }
            else
            {
                // Default behavior based on system theme
                var theme = AppInfo.RequestedTheme;
                Behaviors.Add(new StatusBarBehavior
                {
                    StatusBarColor = theme == AppTheme.Dark ? Colors.Black : Colors.White,
                    StatusBarStyle = theme == AppTheme.Dark
                        ? StatusBarStyle.LightContent
                        : StatusBarStyle.DarkContent
                });
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var existing = Behaviors.OfType<StatusBarBehavior>().FirstOrDefault();
            if (existing != null)
                Behaviors.Remove(existing);
        }
    }
}
