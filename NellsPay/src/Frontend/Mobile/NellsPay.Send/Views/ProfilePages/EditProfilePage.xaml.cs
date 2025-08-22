

using NellsPay.Send.ViewModels.EditProfileVM;

namespace NellsPay.Send.Views.ProfilePages;

public partial class EditProfilePage : ContentPage
{
	public EditProfilePage(EditProfileVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		viewModel.RegisterOpenPickerAction(() => CountryPicker.Focus());
	}
	
	private void OnPhoneNumberTextChanged(object sender, TextChangedEventArgs e)
	{
		var entry = (Entry)sender;
		string digitsOnly = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

		if (entry.Text != digitsOnly)
			entry.Text = digitsOnly;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is EditProfileVM vm)
		{
			await vm.GetUserInfo();
		}
	}
}