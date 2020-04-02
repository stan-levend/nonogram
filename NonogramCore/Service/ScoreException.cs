using System;

namespace nonogram.Service
{
    class ScoreException : Exception
    {
        public ScoreException(string message) : base(message)
        {
        }

        public ScoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
