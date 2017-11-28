using System;
using System.Runtime.Serialization;

namespace BLL
{
    [Serializable]
    public class DublicateUserException : Exception
    {
        public DublicateUserException()
        {
        }

        public DublicateUserException(string message) : base(message)
        {
        }

        public DublicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DublicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}