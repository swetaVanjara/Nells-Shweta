using System;
using System.Text.Json.Serialization;

namespace NellsPay.Send.Contracts
{

    public partial class SessionWrapper
    {
        public Session Session { get; set; }
    }

    public partial class Session
    {
        public Uri Callback { get; set; }
        public string VendorData { get; set; }
    }

    public partial class SubmitSessionWrapper
    {
        public string status { get; set; }
    }
}
