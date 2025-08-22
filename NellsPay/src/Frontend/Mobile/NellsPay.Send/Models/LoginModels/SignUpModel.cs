using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NellsPay.Send.Models.LoginModels;

namespace NellsPay.Send.Models.LoginModels
{
    public class SignUpModel : BaseViewModel
    {
        private string _Email { get; set; } = string.Empty;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                if (_Email.Length > 0)
                {
                    EmailStatus = 1;
                }
                else
                {
                    EmailStatus = 0;
                }
                OnPropertyChanged();
            }
        }
        private string _FirstName { get; set; } = string.Empty;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                if (_FirstName.Length > 0)
                {
                    FirstNameStatus = 1;
                }
                else
                {
                    FirstNameStatus = 0;
                }
                OnPropertyChanged();
            }
        }
        private string _LastName { get; set; } = string.Empty;
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                if (_LastName.Length > 0)
                {
                    LastNameStatus = 1;
                }
                else
                {
                    LastNameStatus = 0;
                }
                OnPropertyChanged();
            }
        }
        private string _Password { get; set; } = string.Empty;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                if (_Password.Length > 0)
                {
                    PasswordStatus = 1;
                }
                else
                {
                    PasswordStatus = 0;
                }
                OnPropertyChanged();
            }
        }
        private string _ReEnterPassword { get; set; } = string.Empty;
        public string ReEnterPassword
        {
            get => _ReEnterPassword;
            set
            {
                _ReEnterPassword = value;
                if (_ReEnterPassword.Length > 0)
                {
                    ReEnterPasswordStatus = 1;
                }
                else
                {
                    ReEnterPasswordStatus = 0;
                }
                OnPropertyChanged();
            }
        }

        //ui 0 = Normal. 1 = Active. 2 = Error
        private int _EmailStatus { get; set; } = 0;
        public int EmailStatus
        {
            get => _EmailStatus;
            set
            {
                _EmailStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _FirstNameStatus { get; set; } = 0;
        public int FirstNameStatus
        {
            get => _FirstNameStatus;
            set
            {
                _FirstNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _LastNameStatus { get; set; } = 0;
        public int LastNameStatus
        {
            get => _LastNameStatus;
            set
            {
                _LastNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _PasswordStatus { get; set; } = 0;
        public int PasswordStatus
        {
            get => _PasswordStatus;
            set
            {
                _PasswordStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _ReEnterPasswordStatus { get; set; } = 0;
        public int ReEnterPasswordStatus
        {
            get => _ReEnterPasswordStatus;
            set
            {
                _ReEnterPasswordStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }

        private bool _Activation { get; set; } = false;
        public bool Activation
        {
            get => _Activation;
            set
            {
                _Activation = value;
                OnPropertyChanged();
            }
        }

        private void CheckActivation()
        {
            if (EmailStatus == 1 && FirstNameStatus == 1 && LastNameStatus == 1 && PasswordStatus == 1 && ReEnterPasswordStatus == 1)
            {
                Activation = true;
            }
            else
            {
                Activation = false;
            }
        }
    }
}

