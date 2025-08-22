using System;
using System.Runtime.Serialization;

namespace NellsPay.Send.Exceptions
{
    [Serializable]
    public class ServerErrorException : Exception
    {
        public ServerErrorException()
        {
        }

        protected ServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ServerErrorException(string message) : base(message)
        {
        }

        public ServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

