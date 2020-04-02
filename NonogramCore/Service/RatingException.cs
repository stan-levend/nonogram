using System;

namespace nonogram.Service
{
    class RatingException : Exception
    {
        public RatingException(string message) : base(message)
        {
        }

        public RatingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
