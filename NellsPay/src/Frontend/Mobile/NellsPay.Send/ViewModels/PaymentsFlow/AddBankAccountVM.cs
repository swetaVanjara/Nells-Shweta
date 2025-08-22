using NellsPay.Send.Models.PamentFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.PaymentsFlow
{
    [QueryProperty(nameof(Bank), "Bank")]
    public  class AddBankAccountVM : BaseViewModel
    {
        #region Fields
        private BankModel _Bank { get; set; } = new BankModel();
        private BankAccountModel _BankAccount { get; set; } = new BankAccountModel();
        private ObservableCollection<BankModel> _Banks { get; set; } = new ObservableCollection<BankModel>();
        #endregion
        #region Property
        public BankModel Bank
        {
            get => _Bank;
            set
            {
                _Bank = value;
  
                OnPropertyChanged();
            }
        }
        public BankAccountModel BankAccount
        {
            get => _BankAccount;
            set
            {
                _BankAccount = value;
               
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BankModel> Banks
        {
            get => _Banks;
            set
            {
                _Banks = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Extra
        private readonly IPaymentFlowService _Service;
        #endregion
        public AddBankAccountVM(IPaymentFlowService serviceProvider)
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
            Banks = new ObservableCollection<BankModel>(await _Service.GetAvailableBanks());
        }
        #endregion


        #region Commands


        public ICommand BackCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync("..");


        });
        public ICommand AddBankCommand => new Command( () =>
        {

           //
        });
        #endregion

    }
}
