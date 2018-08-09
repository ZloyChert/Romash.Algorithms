using System;
using System.Runtime.Serialization;

namespace SearchTrees.Exceptions
{
    public class SearchTreeException : Exception
    {
        public SearchTreeException()
        {
        }

        public SearchTreeException(string message) : base(message)
        {
        }

        public SearchTreeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
