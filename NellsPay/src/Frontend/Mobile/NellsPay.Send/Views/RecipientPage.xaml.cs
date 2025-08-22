namespace NellsPay.Send.Views;

public partial class RecipientPage : ContentPage
{
    public RecipientPage(RecipientViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void backbutton_Clicked(object sender, EventArgs e)
    {

    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is RecipientViewModel vm)
        {
            await vm.OnAppearingAsync(); 
        }
    }
}