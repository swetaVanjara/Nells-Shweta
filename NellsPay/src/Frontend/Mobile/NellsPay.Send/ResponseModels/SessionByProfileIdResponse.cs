using System;
namespace NellsPay.Send.ResponseModels
{
    public partial class SessionByProfileIdResponse : ApiErrorResponse
    {
        public VeriffSession VeriffSession { get; set; }
    }

    public partial class VeriffSession
    {
        public Guid ProfileId { get; set; }
        public string SessionId { get; set; }
        public string Url { get; set; }
        public string VendorData { get; set; }
        public string Host { get; set; }
        public string Status { get; set; }
        public string SessionToken { get; set; }
    }
}