using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Models.RecipientsModels;
using NellsPay.Send.ViewModels.RecipientsViewModels;

namespace NellsPay.Send.Views.RecipientsPages;



public partial class AddEditRecipientPage : ContentPage
{
    public AddEditRecipientPage(AddEditRecipientVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddEditRecipientVM vm)
        {
            _ = vm.OnPageAppearing();
        }
    }
    
}