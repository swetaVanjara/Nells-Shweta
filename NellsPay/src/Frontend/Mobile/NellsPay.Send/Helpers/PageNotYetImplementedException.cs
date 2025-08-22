using System;
using System.Runtime.Serialization;

namespace NellsPay.Send.Navigation
{
    [Serializable]
    public class PageNotYetImplementedException : Exception
    {
        public PageNotYetImplementedException()
        {
        }

        protected PageNotYetImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PageNotYetImplementedException(string message) : base(message)
        {
        }

        public PageNotYetImplementedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}