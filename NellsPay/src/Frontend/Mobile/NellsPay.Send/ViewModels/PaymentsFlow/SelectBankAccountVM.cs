using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Models.PamentFlow;
using NellsPay.Send.Views.PaymentsFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.PaymentsFlow
{
    [QueryProperty(nameof(Bank), "Bank")]
    [QueryProperty(nameof(TransferPin), "TransferPin")]
    public class SelectBankAccountVM : BaseViewModel
    {
        #region Fields
        private TransferPinModel _transferPin { get; set; }
        private ObservableCollection<BankAccountModel> _BankAccounts { get; set; } = new ObservableCollection<BankAccountModel>();
        private BankModel _Bank { get; set; } = new BankModel();
        #endregion
        #region Property
        public TransferPinModel TransferPin
        {
            get { return _transferPin; }
            set
            {
                if (_transferPin != value)
                {
                    _transferPin = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<BankAccountModel> BankAccounts
        {
            get => _BankAccounts;
            set
            {
                _BankAccounts = value;
                OnPropertyChanged();
            }
        }
        public BankModel Bank
        {
            get => _Bank;
            set
            {
                _Bank = value;
                Task.Run(async () =>
                {
                    await GetData(_Bank);
                });
                OnPropertyChanged();
            }
        }
        #endregion
        #region Extra
        private readonly IPaymentFlowService _Service;
        #endregion
        public SelectBankAccountVM(IPaymentFlowService serviceProvider)
        {
            _Service = serviceProvider;

        }

        #region  Methods
        public async Task GetData(BankModel bank)
        {
            BankAccounts = new ObservableCollection<BankAccountModel>(await _Service.GetAvailableBankAccounts(bank.Id));
        }
        #endregion


        #region Commands
        public ICommand SelectedCommand
        {
            get
            {
                return new Command(async(e) =>
                {

                    var item = (e as BankAccountModel);

                    ConfirmpaymentModel Confirmpayment = new ConfirmpaymentModel()
                    {
                        Amount = TransferPin.Transferamount,
                        Accountnumber = item.AccountNumber,
                        Bankname = item.Bank.BankName,
                        Payeename = TransferPin.Sender,
                        PaymentMethod = "Bank Account"

                    };
                    await Shell.Current.GoToAsync($"{nameof(ConfirmPaymentPage)}?",
                    new Dictionary<string, object>
                    {
                        ["Confirmpayment"] = Confirmpayment,
                    });
                    //await Shell.Current.GoToAsync($"{nameof(TransferPinPage)}?",
                    //new Dictionary<string, object>
                    //{
                    //    ["Transfer"] = PaymentMethod,
                    //});

                });

            }

        }

        public ICommand BackCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync("..");


        });
        public ICommand AddBankCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync(nameof(AddBankAccountPage));


        });
        #endregion

    }
}
