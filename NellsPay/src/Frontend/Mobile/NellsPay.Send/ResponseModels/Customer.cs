using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.ResponseModels
{
    public class CustomerResponse : ApiErrorResponse
    {
        public Customer Customer { get; set; } = new Customer();
    }
    public class Customer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
        public string? country2Code { get; set; }
        public string? Status { get; set; }
    }
    public partial class EditCustomerResponse
    {
        public bool IsScuccess { get; set; }
    }
}