using System;
using nonogram.States;

namespace nonogram.Core
{
    public class Grid {
        
        public int xSizeGrid { get; private set;}
        public int ySizeGrid { get; private set;}
        private Tile[,] tiles;
        private int[,] horizontal;
        private int[,] vertical;
        private Legend legend;
        private char[,] chosenImage;
        public GameState CurrentState { get; set; }

        public Grid(char[,] chosenImage)
        {
            this.chosenImage = chosenImage;

            this.xSizeGrid = chosenImage.GetLength(1);
            this.ySizeGrid = chosenImage.GetLength(0);

            tiles = new Tile[ySizeGrid, xSizeGrid];
            InitializeAndMapTiles();
            GenerateLegend();
        }

        private void InitializeAndMapTiles() 
        {
            for (int i = 0; i < ySizeGrid; i++)
            {
                for (int j = 0; j < xSizeGrid; j++)
                {
                    tiles[i,j] = new Tile();
                    if(chosenImage[i,j] == '#') tiles[i,j].Initial = TileState.Colored;
                    else if (chosenImage[i,j] == '.') tiles[i,j].Initial = TileState.Blank;
                    tiles[i,j].Input = TileState.Hidden;
                }
            }
        }
        private void GenerateLegend()
        {
            legend = new Legend(chosenImage);
            vertical = legend.GenerateVertical();
            horizontal = legend.GenerateHorizontal();
        }

        public void Solve() 
        {
            foreach (var tile in tiles)
            {
                tile.Input = tile.Initial;
            }
            CurrentState = GameState.Lost;
        }

        public bool IsSolved()
        {
            foreach (var tile in tiles)
            {
                if(tile.Initial != tile.Input) return false;
            }
            CurrentState = GameState.Solved;
            return true;
        }

        public void RevealHint() 
        {
            var random = new Random();
            var x = random.Next(xSizeGrid);
            var y = random.Next(ySizeGrid);
            if(tiles[y,x].Input == tiles[y,x].Initial) RevealHint();
            tiles[y,x].Input = tiles[y,x].Initial;
        }

        public void BlankTile(int x, int y) 
        {
            tiles[y,x].Input = TileState.Blank;
        }
        public void MarkTile(int x, int y) 
        {
            tiles[y,x].Input = TileState.Colored;
        }

        public void Print()
        {
            //PRINT GRID + VERTICAL LEGEND
            for (int i = 0; i < ySizeGrid; i++)
            {
                for (int j = 0; j < xSizeGrid; j++)
                {
                    Console.Write("  ");
                    if(tiles[i,j].Input == TileState.Colored) Console.Write("#");
                    else if (tiles[i,j].Input == TileState.Blank) Console.Write(".");
                    else if (tiles[i,j].Input == TileState.Hidden) Console.Write(" ");
                }
                Console.Write("| ");

                //VERTICAL LEGEND
                for (int j = 0; j < xSizeGrid / 2 + 1; j++)
                {
                    if(vertical[i,j] == 0) continue;
                    else Console.Write(String.Format("{0,-3}", vertical[i,j]));
                }
                Console.WriteLine();
            }

            //PRINT LINE
            for (int i = 0; i < xSizeGrid; i++) Console.Write("---");
            Console.WriteLine();

            //PRINT HORIZONTAL LEGEND
            for (int i = 0; i < ySizeGrid / 2 + 1; i++)
            {
                int zeros = 0;
                for (int j = 0; j < xSizeGrid; j++)
                {
                    if(horizontal[i,j] == 0) 
                    {
                        Console.Write("   ");
                        zeros++;
                    }
                    else Console.Write(String.Format("{0,3}", horizontal[i,j]));
                }
                Console.WriteLine();
                if(zeros == xSizeGrid) break;
            }
        }
    }
}

