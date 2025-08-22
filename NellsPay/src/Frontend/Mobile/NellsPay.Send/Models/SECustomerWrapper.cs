using CommunityToolkit.Maui.Core;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Models
{ 
    public partial class AddSeCustomerWrapper
    {
        // public Guid CustomerId { get; set; }
        public CustomerRequest CustomerRequest { get; set; }
    }

    public partial class CustomerRequest
    {
        public string Email { get; set; }
        public string CitizenshipCode { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string ResidentAddress { get; set; }
    }
}