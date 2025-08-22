using CommunityToolkit.Maui.Core;
using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Models
{
    public class RecipientWrapper
    {
        public Recipient Recipient { get; set; }
    }
   public class Recipient : ObservableObject
    {
        [PrimaryKey] public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string Email { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PhoneCode { get; set; } = default!;
        public string Initials { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string CountryFlag { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string PayOutType { get; set; } = default!;
        public string PayOutAccount { get; set; } = default!;
        public string AddressLine1 { get; set; }= default!;
        public string AddressLine2 { get; set; }= default!;
        public string City { get; set; }= default!;
        public string Region { get; set; }= default!;
        public string PostCode { get; set; }= default!;
        private bool isFavorite { get; set; } = false;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                    OnPropertyChanged();

                }
            }
        }

    }
}
