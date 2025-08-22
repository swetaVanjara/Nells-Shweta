using NellsPay.Send.Models.MoneyTransferFlowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.MoneyTransferFlowViewModels
{
    [QueryProperty(nameof(TransactionDetails), "TransactionDetails")]
    public partial class TransferPinVM : BaseViewModel
    {
        #region Fields
        private readonly ICurrencyTransferService _currencyTransferService;

        [ObservableProperty] private TransactionDetailModel transactionDetails;
        #endregion

        public TransferPinVM(ICurrencyTransferService currencyTransferService)
        {
            _currencyTransferService = currencyTransferService;
            TransactionDetails = new TransactionDetailModel()
            {
                SenderCurrency = _currencyTransferService.Sender.CurrencyCode,
                SenderFlag = _currencyTransferService.Sender.CountryFlag,
                ReciverFlag = _currencyTransferService.Reciever.CountryFlag,
                Reasonoftransaction = _currencyTransferService.ReviewTransictionData.Reason,
                Deliverymethod = _currencyTransferService.ReviewTransictionData.Recipient.PayOutType,
                TransferDate = DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture),
                TransferTime = DateTime.Now.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture),
                RecipientName = _currencyTransferService.ReviewTransictionData.Recipient.FullName,
                Status = "Successfull",
                TransactionID = _currencyTransferService.ReviewTransictionData.Id.ToString(),
                TransactionDate = DateTime.Now,
                Transferamount = _currencyTransferService.ReviewTransictionData.Transferamount.ToString(),
                Transactionnumber = _currencyTransferService.ReviewTransictionData.TransactionNumber,
                Totaltorecipient = _currencyTransferService.ReviewTransictionData.Totaltorecipient,
                Amount = _currencyTransferService.ReviewTransictionData.Transferamount.ToString(),
            };
        }

        public ICommand BackCommand => new Command(async () =>
        {
            BackToHome();
        });

        private void BackToHome()
        {
            _currencyTransferService.SelectedPaymentMethod = null;
            _currencyTransferService.ReviewTransictionData = null;
            _currencyTransferService.SaltbyemailResponse = null;

            Application.Current.MainPage = new AppShell();
        }

        public ICommand DownloadCommand => new Command(async () =>
        {
            BackToHome();
        });
        public ICommand HomeCommand => new Command(async () =>
        {
            BackToHome();
        });

    }
}
