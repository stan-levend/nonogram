using System;

namespace nonogram.Service
{
    class CommentException : Exception
    {
        public CommentException(string message) : base(message)
        {
        }

        public CommentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
