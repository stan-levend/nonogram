using System;

namespace nonogram.Core
{
    [Serializable]
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