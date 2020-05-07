using System;

namespace nonogram.Core
{
    [Serializable]
    public class Grid {
        
        public int xSize { get; private set; }
        public int ySize { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public GameState CurrentState { get; set; }
        private char[,] chosenImage;
        private DateTime startTime;

        public Grid(char[,] chosenImage)
        {
            this.chosenImage = chosenImage;
            xSize = chosenImage.GetLength(1);
            ySize = chosenImage.GetLength(0);

            Tiles = new Tile[ySize, xSize];
            InitializeAndMapTiles();
            startTime = DateTime.Now;
            CurrentState = GameState.Playing;
        }

        private void InitializeAndMapTiles() 
        {
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Tiles[i,j] = new Tile();
                    if(chosenImage[i,j] == '#') Tiles[i,j].Actual = TileState.Colored;
                    else if (chosenImage[i,j] == '.') Tiles[i,j].Actual = TileState.Blank;
                    Tiles[i,j].Input = TileState.Hidden;
                }
            }
        }

        public void BlankTile(int x, int y) 
        {
            Tiles[y,x].Input = TileState.Blank;
            IsWon();
        }

        public void MarkTile(int x, int y) 
        {
            Tiles[y,x].Input = TileState.Colored;
            IsWon();
        }

        public void Clear()
        {
            foreach (var tile in Tiles)
            {
                tile.Input = TileState.Hidden;
            }
        }

        public void Solve() 
        {
            foreach (var tile in Tiles)
            {
                tile.Input = tile.Actual;
            }
            CurrentState = GameState.Lost;
        }

        public bool IsWon()
        {
            foreach (var tile in Tiles)
            {
                if(tile.Actual != tile.Input) return false;
            }
            CurrentState = GameState.Won;
            return true;
        }

        public void RevealHint() 
        {
            var random = new Random();
            var x = random.Next(xSize);
            var y = random.Next(ySize);
            if(Tiles[y,x].Input == Tiles[y,x].Actual) RevealHint();
            Tiles[y,x].Input = Tiles[y,x].Actual;
            IsWon();
        }

        public int GetScore()
        {
            return xSize * ySize * 30 - (DateTime.Now - startTime).Seconds;
        }

    }
}

