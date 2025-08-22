using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NellsPay.Send.Models.RecipientsModels
{
    public class AddEditRecipientModel : BaseModel
    {
        private Data _selectedCountryData = null;
        public Data SelectedCountryData
        {
            get => _selectedCountryData;
            set
            {
                _selectedCountryData = value;
                OnPropertyChanged();
            }
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                FirstNameStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                LastNameStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                FullNameStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                PhoneNumberStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _deliveryMethod = string.Empty;
        public string DeliveryMethod
        {
            get => _deliveryMethod;
            set
            {
                _deliveryMethod = value;
                DeliveryMethodStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                AddressStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _accountNumber = string.Empty;
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                _accountNumberStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                PostalCodeStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                EmailStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        private string _countryName = string.Empty;
        public string CountryName
        {
            get => _countryName;
            set
            {
                _countryName = value;
                CountryNameStatus = string.IsNullOrWhiteSpace(value) ? 0 : 1;
                OnPropertyChanged();
            }
        }

        // Status properties: 0 = normal, 1 = valid, 2 = error
        private int _firstNameStatus { get; set; } = 0;
        public int FirstNameStatus
        {
            get => _firstNameStatus;
            set
            {
                _firstNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _lastNameStatus { get; set; } = 0;
        public int LastNameStatus
        {
            get => _lastNameStatus;
            set
            {
                _lastNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _fullNameStatus { get; set; } = 0;
        public int FullNameStatus
        {
            get => _fullNameStatus;
            set
            {
                _fullNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _phoneNumberStatus { get; set; } = 0;
        public int PhoneNumberStatus
        {
            get => _phoneNumberStatus;
            set
            {
                _phoneNumberStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _deliveryMethodStatus { get; set; } = 0;
        public int DeliveryMethodStatus
        {
            get => _deliveryMethodStatus;
            set
            {
                _deliveryMethodStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _addressStatus { get; set; } = 0;
        public int AddressStatus
        {
            get => _addressStatus;
            set
            {
                _addressStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }

        private int _accountNumberStatus { get; set; } = 0;
        public int AccountNumberStatus
        {
            get => _accountNumberStatus;
            set
            {
                _accountNumberStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _postalCodeStatus { get; set; } = 0;
        public int PostalCodeStatus
        {
            get => _postalCodeStatus;
            set
            {
                _postalCodeStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _emailStatus { get; set; } = 0;
        public int EmailStatus
        {
            get => _emailStatus;
            set
            {
                _emailStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        private int _countryNameStatus { get; set; } = 0;
        public int CountryNameStatus
        {
            get => _countryNameStatus;
            set
            {
                _countryNameStatus = value;
                CheckActivation();
                OnPropertyChanged();
            }
        }
        
        private bool _activation = false;
        public bool Activation
        {
            get => _activation;
            set
            {
                _activation = value;
                OnPropertyChanged();
            }
        }

        private void CheckActivation()
        {
            Activation = FirstNameStatus == 1 &&
                         LastNameStatus == 1 &&
                         FullNameStatus == 1 &&
                         PhoneNumberStatus == 1 &&
                         DeliveryMethodStatus == 1 &&
                         AccountNumberStatus == 1 &&
                         PostalCodeStatus == 1 &&
                         EmailStatus == 1;
        }
    }
}
