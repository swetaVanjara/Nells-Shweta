using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class UserModel : BaseViewModel
    {
        private string _UserFirstName { get; set; } = default!;
        public string UserFirstName
        {
            get { return _UserFirstName; }
            set
            {
                if (_UserFirstName != value)
                {
                    _UserFirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _UserLastName { get; set; } = default!;
        public string UserLastName
        {
            get { return _UserLastName; }
            set
            {
                if (_UserLastName != value)
                {
                    _UserLastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _UserName { get; set; } = default!;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _UserImage { get; set; } = default!;
        public string UserImage
        {
            get { return _UserImage; }
            set
            {
                if (_UserImage != value)
                {
                    _UserImage = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Email { get; set; } = default!;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _postCode { get; set; } = default!;
        public string PostCode
        {
            get { return _postCode; }
            set
            {
                if (_postCode != value)
                {
                    _postCode = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city { get; set; } = default!;
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country { get; set; } = default!;
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Region { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateOnly BirthDate { get; set; } = new DateOnly(1997, 3, 22);
    }
}
