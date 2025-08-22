using System;
using System.Text.Json.Serialization;
namespace NellsPay.Send.ResponseModels
{ 
    public partial class ProfileByIdResponse
    {
        public Profile Profile { get; set; }
    }

    public partial class Profile
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Kyc Kyc { get; set; }
        public string ComplianceStatus { get; set; }
        public Decision Decision { get; set; }
        public Vendor Vendor { get; set; }
        public string EventType { get; set; }
        public DateTimeOffset AcceptanceTime { get; set; }
    }

}