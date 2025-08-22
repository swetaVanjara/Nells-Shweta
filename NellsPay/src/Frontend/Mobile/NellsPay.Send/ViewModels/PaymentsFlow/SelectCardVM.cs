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
    [QueryProperty(nameof(TransferPin), "TransferPin")]
    public class SelectCardVM : BaseViewModel
    {
        #region Fields
        private TransferPinModel _transferPin { get; set; }
        private ObservableCollection<CardsModel> _Cards { get; set; } = new ObservableCollection<CardsModel>();

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
        public ObservableCollection<CardsModel> Cards
        {
            get => _Cards;
            set
            {
                _Cards = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Extra
        private readonly IPaymentFlowService _Service;
        #endregion
        public SelectCardVM(IPaymentFlowService serviceProvider)
        {
            _Service = serviceProvider;
            Task.Run(async () =>
            {
                await GetData();
            });
        }

        #region  Methods
        public async Task GetData()
        {
            Cards = new ObservableCollection<CardsModel>(await _Service.GetAvailableCards());
        }
        #endregion


        #region Commands
        public ICommand SelectedCommand
        {
            get
            {
                return new Command( async (e) =>
                {

                    var item = (e as CardsModel);

                    ConfirmpaymentModel Confirmpayment = new ConfirmpaymentModel()
                    {
                        Amount = TransferPin.Transferamount,
                        Accountnumber = item.CardNumber,
                        Bankname = item.CardType,
                        Payeename = TransferPin.Sender,
                        PaymentMethod = "Debit/credit card"

                    };
                    await Shell.Current.GoToAsync($"{nameof(ConfirmPaymentPage)}?",
                    new Dictionary<string, object>
                    {
                        ["Confirmpayment"] = Confirmpayment,
                    });

                });

            }

        }

        public ICommand BackCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync("..");


        });
        #endregion

    }
}
