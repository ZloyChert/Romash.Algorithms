using System;
using System.Runtime.Serialization;

namespace SearchTrees.Exceptions
{
    class SearchTreeArgumentException : SearchTreeException
    {
        public SearchTreeArgumentException()
        {
        }

        public SearchTreeArgumentException(string message) : base(message)
        {
        }

        public SearchTreeArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchTreeArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
