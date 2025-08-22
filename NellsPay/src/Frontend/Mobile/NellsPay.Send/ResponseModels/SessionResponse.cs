using System;
namespace NellsPay.Send.ResponseModels
{
    public partial class SessionResponse
    {
        public Uri Url { get; set; }
    }

    public partial class StartSessionResponse
    {
        public string Message { get; set; }
    }
}