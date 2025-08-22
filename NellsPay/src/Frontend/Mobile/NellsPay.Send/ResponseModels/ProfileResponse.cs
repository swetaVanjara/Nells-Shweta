using System;
namespace NellsPay.Send.ResponseModels
{
    public partial class ProfileResponse
    {
        public Profiles? Profiles { get; set; }
    }

    public partial class Profiles
    {
        public long PageIndex { get; set; }
        public long PageSize { get; set; }
        public long Count { get; set; }
        public Datum[]? Data { get; set; }
    }

    public partial class Datum
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Kyc? Kyc { get; set; }
        public ComplianceStatus ComplianceStatus { get; set; }
        public Decision? Decision { get; set; }
        public Vendor? Vendor { get; set; }
        public string? EventType { get; set; }
        public DateTimeOffset AcceptanceTime { get; set; }
    }

    public partial class Decision
    {
        public string? DecisionType { get; set; }
        public long DecisionScore { get; set; }
    }

    public partial class Kyc
    {
        public DateTimeOffset DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? Profession { get; set; }
        public string? ForeignStatus { get; set; }
    }

    public partial class Vendor
    {
        public string? VendorName { get; set; }
        public string? Version { get; set; }
        public string? SessionId { get; set; }
        public string? AttemptId { get; set; }
        public string? VendorId { get; set; }
    }

    public enum ComplianceStatus { Pending };
}