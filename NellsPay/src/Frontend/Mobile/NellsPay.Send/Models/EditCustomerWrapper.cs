using CommunityToolkit.Maui.Core;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Models
{ 
    public partial class EditCustomerWrapper
    {
        public Customer Customer { get; set; }
    }

    public partial class Customer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public string Country2Code { get; set; }
        public string Status { get; set; }
        public string Initials { get; set; }
        public string Country { get; set; }
    }
}