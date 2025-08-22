using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.LoginViewModels
{
    public class PINVM : BaseViewModel
    {
        #region Fields

        private ObservableCollection<string> _Images { get; set; }
        #endregion
        #region Property


        public ObservableCollection<string> Images
        {
            get => _Images;
            set
            {
                _Images = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public PINVM()
        {
           
        }



    }
}
