using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace NellsPay.Send.Navigation
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class NotInitializableException : Exception
    {
        public NotInitializableException()
        {
        }

        protected NotInitializableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotInitializableException(string message) : base(message)
        {
        }

        public NotInitializableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}