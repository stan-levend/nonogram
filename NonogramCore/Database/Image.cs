namespace nonogram.Database
{
    public abstract class Image
    {
        public string Name { get; protected set; }
        public char[,] Data { get; protected set; }
    }
}