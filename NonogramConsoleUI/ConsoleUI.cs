using System;
using nonogram.Core;
using nonogram.States;

namespace nonogram.ConsoleUI
{
    public class ConsoleUI {
        private Grid grid;
        private Legend legend;
        private Database database;
        private static int hints = 10;
        public ConsoleUI() 
        {
            database = new Database();
        }
        public void Run() 
        {
            Initialize();
            grid.CurrentState = GameState.Playing;

            do 
            {
                Print();
                ProcessInput();
            } 
            while (grid.CurrentState == GameState.Playing);

            Print();
            if(grid.CurrentState == GameState.Won) Console.WriteLine("Congratulations, you have solved the puzzle");
            else if(grid.CurrentState == GameState.Lost) Console.WriteLine("Better luck next time.");
        }

        private void Initialize()
        {
            var chosenImage = database.ChooseRandomImage();
            grid = new Grid(chosenImage);
            legend = new Legend(chosenImage);
        }
        private void ProcessInput()
        {
            Console.WriteLine("(S)olve, (H)ints: {0}", hints);
            Console.Write("(#)Mark, (.)Blank: ");
            try
            {
                string[] input = Console.ReadLine().ToLower().Split();
                var parsedInput = char.Parse(input[0]);

                if (parsedInput == 's')
                {
                    grid.Solve();
                    return;
                }
                else if (parsedInput == 'h')
                {
                    if(hints == 0) 
                    {
                        PrintError("No more hints available");
                        return;
                    }
                    hints--;
                    grid.RevealHint();
                }
                else if(parsedInput == '#' || parsedInput == '.') 
                {
                    var x = int.Parse(input[1]);
                    var y = int.Parse(input[2]);

                    if(parsedInput == '#' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.MarkTile(x,y);
                    if(parsedInput == '.' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.BlankTile(x,y);
                } 
                grid.IsWon();
            }
            catch (Exception) 
            {
                PrintError("Wrong input!");
            }
        }
        public void Print()
        {
            //print grid, vertical legend
            for (int i = 0; i < grid.ySize; i++)
            {
                for (int j = 0; j < grid.xSize; j++)
                {
                    Console.Write("  ");
                    if(grid.Tiles[i,j].Input == TileState.Colored) Console.Write("#");
                    else if (grid.Tiles[i,j].Input == TileState.Blank) Console.Write(".");
                    else if (grid.Tiles[i,j].Input == TileState.Hidden) Console.Write(" ");
                }
                Console.Write("| ");

                //vertical legend
                for (int j = 0; j < grid.xSize / 2 + 1; j++)
                {
                    if(legend.vertical[i,j] == 0) continue;
                    else Console.Write(String.Format("{0,-3}", legend.vertical[i,j]));
                }
                Console.WriteLine();
            }

            //print horizontal line
            for (int i = 0; i < grid.xSize; i++) Console.Write("---");
            Console.WriteLine();

            //print horizontal legend
            for (int i = 0; i < grid.ySize / 2 + 1; i++)
            {
                int zeros = 0;
                for (int j = 0; j < grid.xSize; j++)
                {
                    if(legend.horizontal[i,j] == 0) 
                    {
                        Console.Write("   ");
                        zeros++;
                    }
                    else Console.Write(String.Format("{0,3}", legend.horizontal[i,j]));
                }
                Console.WriteLine();
                if(zeros == grid.xSize) break;
            }
        }
        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}

