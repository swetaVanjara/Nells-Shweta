namespace NellsPay.Send.ResponseModels
{
    public partial class SaltbyemailResponse
    {
        public Guid CustomerId { get; set; }
        public string SeCustomerId { get; set; }
        public string Identifier { get; set; }
        public string Email { get; set; }
    }
}