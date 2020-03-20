using System;
using nonogram.States;

namespace nonogram.Core
{
    public class Tile
    {
        public Tile()
        {
        }

        public TileState Initial
        {
            get; set;
        }

        public TileState Input
        {
            get; set;
        }
    }

}