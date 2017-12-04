using System;
using System.Runtime.Serialization;

namespace BLL
{
    [Serializable]
    public class DateInPastException : Exception
    {
        public DateInPastException()
        {
        }

        public DateInPastException(string message) : base(message)
        {
        }

        public DateInPastException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateInPastException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}