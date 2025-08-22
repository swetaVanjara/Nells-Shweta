using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.ProfileViewModels
{
    public partial class ChangePasswordVM : BaseViewModel
    {
        #region Fields
        private string _CurrentPassword { get; set; } = string.Empty;
        private string _NewPassword { get; set; } = string.Empty;
        private string _RetypePassword { get; set; } = string.Empty;

        private bool _CurrentShow { get; set; } = false;
        private bool _NewShow { get; set; } = false;
        private bool _ReShow { get; set; } = false;
        private string _Error { get; set; } = string.Empty;
        private bool _ErrorShow { get; set; } = false;
        #endregion
        #region Property
        public string CurrentPassword
        {
            get { return _CurrentPassword; }
            set
            {
                if (_CurrentPassword != value)
                {
                    _CurrentPassword = value;
                    OnPropertyChanged();

                }
            }
        }
        public string NewPassword
        {
            get { return _NewPassword; }
            set
            {
                if (_NewPassword != value)
                {
                    _NewPassword = value;
                    OnPropertyChanged();

                }
            }
        }
        public string RetypePassword
        {
            get { return _RetypePassword; }
            set
            {
                if (_RetypePassword != value)
                {
                    _RetypePassword = value;
                    OnPropertyChanged();

                }
            }
        }

        public bool CurrentShow
        {
            get { return _CurrentShow; }
            set
            {
                if (_CurrentShow != value)
                {
                    _CurrentShow = value;
                    OnPropertyChanged();

                }
            }
        }
        public bool NewShow
        {
            get { return _NewShow; }
            set
            {
                if (_NewShow != value)
                {
                    _NewShow = value;
                    OnPropertyChanged();

                }
            }
        }
        public bool ReShow
        {
            get { return _ReShow; }
            set
            {
                if (_ReShow != value)
                {
                    _ReShow = value;
                    OnPropertyChanged();

                }
            }
        }
        public string Error
        {
            get { return _Error; }
            set
            {
                if (_Error != value)
                {
                    _Error = value;
                    OnPropertyChanged();

                }
            }
        }
        public bool ErrorShow
        {
            get { return _ErrorShow; }
            set
            {
                if (_ErrorShow != value)
                {
                    _ErrorShow = value;
                    OnPropertyChanged();

                }
            }
        }

        #endregion

        public ChangePasswordVM()
        {

        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                Error = "Current Password is required";
                ErrorShow = true;
                return false;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                Error = "New Password is required";
                ErrorShow = true;
                return false;
            }
            if (string.IsNullOrEmpty(RetypePassword))
            {
                Error = "Retype Password is required";
                ErrorShow = true;
                return false;
            }
            if (NewPassword != RetypePassword)
            {
                Error = "Password does not match";
                ErrorShow = true;
                return false;
            }
            return true;
        }
        public ICommand BackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
        public ICommand SaveCommand => new Command(() =>
        {
            ErrorShow = false;
            bool isValid = Validate();
            if (isValid)
            {
                // TODO: Call API. this for testing
                if (CurrentPassword == "123456")
                {
                    ErrorShow = false;
                    Error = "Password does not match";
                }
                ErrorShow = true;
                // TODO: Call API
            }
        });
        public ICommand CurrentShowCommand => new Command(() =>
        {
            CurrentShow = !CurrentShow;
        });
        public ICommand NewShowCommand => new Command(() =>
        {
            NewShow = !NewShow;
         
        });
        public ICommand ReShowCommand => new Command(() =>
        {
            ReShow = !ReShow;
   
        });

    }
}
