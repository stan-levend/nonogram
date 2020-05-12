using System;

namespace nonogram.Core
{
    [Serializable]
    public class Grid {
        
        public int xSize { get; private set; }
        public int ySize { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public GameState CurrentState { get; set; }
        public int Hints { get; set; } = 10;
        public char[,] ChosenImage { get; private set; }

        private readonly DateTime startTime;

        public Grid(char[,] chosenImage)
        {
            this.ChosenImage = chosenImage;
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
                    if(ChosenImage[i,j] == '#') Tiles[i,j].Actual = TileState.Colored;
                    else if (ChosenImage[i,j] == '.') Tiles[i,j].Actual = TileState.Blank;
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
            TurnTiles();
            CurrentState = GameState.Lost;
        }

        public void IsWon()
        {
            foreach (var tile in Tiles)
            {
                if (tile.Actual == TileState.Colored && tile.Input != TileState.Colored) return;
                if (tile.Input == TileState.Colored && tile.Actual != TileState.Colored) return; 
            }
            TurnTiles();
            CurrentState = GameState.Won;
        }

        public void RevealHint() 
        {
            var random = new Random();
            var x = random.Next(xSize);
            var y = random.Next(ySize);
            if (Tiles[y, x].Input == Tiles[y, x].Actual) 
                RevealHint();
            else
            {
                Tiles[y, x].Input = Tiles[y, x].Actual;
                Hints--;
                IsWon();
            }
        }

        private void TurnTiles()
        {
            foreach (var tile in Tiles)
            {
                tile.Input = tile.Actual;
            }
        }

        public int GetScore()
        {
            return xSize * ySize * 20 - (DateTime.Now - startTime).Seconds + (Hints * 50);
        }

    }
}

