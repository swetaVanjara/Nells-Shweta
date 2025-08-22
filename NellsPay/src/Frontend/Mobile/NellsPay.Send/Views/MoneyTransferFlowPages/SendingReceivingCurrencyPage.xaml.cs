using NellsPay.Send.ViewModels.SendingReceivingCurrencyVM;

namespace NellsPay.Send.Views.MoneyTransferFlowPages;


public partial class SendingReceivingCurrencyPage : ContentPage
{

    public SendingReceivingCurrencyPage(SendingReceivingCurrencyVM viewModel)
	{

        InitializeComponent();
        BindingContext = viewModel;
    


    }



}