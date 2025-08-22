using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Models.PamentFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.PaymentsFlow
{
    [QueryProperty(nameof(Confirmpayment), "Confirmpayment")]
    public class ConfirmPaymentVM : BaseViewModel
    {
        #region Fields

        private ConfirmpaymentModel _Confirmpayment { get; set; } = new ConfirmpaymentModel();
        #endregion
        #region Property


        public ConfirmpaymentModel Confirmpayment
        {
            get => _Confirmpayment;
            set
            {
                _Confirmpayment = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ConfirmPaymentVM()
        {

        }

        #region  Methods
        #endregion


        #region Commands
    

        public ICommand BackCommand => new Command(async () =>
        {

            await Shell.Current.GoToAsync("..");


        });
        public ICommand PayCommand => new Command( () =>
        {

            // Confirmpayment


        });
        #endregion

    }
}
