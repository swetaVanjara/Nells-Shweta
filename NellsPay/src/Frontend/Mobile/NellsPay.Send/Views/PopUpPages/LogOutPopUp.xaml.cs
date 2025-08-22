using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Messages;

namespace NellsPay.Send.Views.PopUpPages;

public partial class LogOutPopUp : Popup
{
	public LogOutPopUp()
	{
		InitializeComponent();
	}
    private void Close_Tapped(object? sender, TappedEventArgs e)
    {
        this.Close();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
    private void LogOur(object sender, EventArgs e)
    {
        this.Close();
        WeakReferenceMessenger.Default.Send(new WeakMessages("LogOut"));
       
    }
}