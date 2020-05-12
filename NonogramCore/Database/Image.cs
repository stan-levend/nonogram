using System;

namespace nonogram.Database
{
    [Serializable]
    public abstract class Image
    {
        public string Name { get; protected set; }
        public char[,] Data { get; set; }
    }
}