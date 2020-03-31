namespace nonogram.Core
{
    public class Tile
    {
        public Tile()
        {
        }

        public TileState Actual
        {
            get; set;
        }

        public TileState Input
        {
            get; set;
        }
    }

}