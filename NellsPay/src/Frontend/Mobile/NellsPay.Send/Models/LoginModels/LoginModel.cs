using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.LoginModels
{
    public class LoginModel : BaseViewModel
    {
        private string _Email { get; set; } = string.Empty;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                if (string.IsNullOrEmpty(value))
                {
                    EmailStatus = 0;
                }
                else
                {
                    EmailStatus = 1;
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
            if (EmailStatus == 1 && PasswordStatus == 1 )
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
