using NellsPay.Send.Views.LoginPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.LoginViewModels
{
    public class OnboardingVM : BaseViewModel
    {
        #region Fields

        private List<string> _Images { get; set; } = new List<string>();
        private int _position { get; set; }

        #endregion
        #region Property

        public List<string> Images
        {
            get => _Images;
            set
            {
                _Images = value;
                OnPropertyChanged();
            }
        }
        public int Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OnboardingVM()
        {
            Images = new List<string>() { "onboarding1.png", "onboarding2.png", "onboarding3.png" };
        }


        public ICommand SignUpCommand => new Command( () =>
        {
           
           
        });
        public ICommand LoginCommand => new Command( () =>
        {
          
           
        });
    }
}
