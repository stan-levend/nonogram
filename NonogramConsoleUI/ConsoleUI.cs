using System;
using nonogram.Core;
using nonogram.States;
using nonogram.Database;

namespace nonogram.ConsoleUI
{
    public class ConsoleUI {
        private Grid grid;
        private Legend legend;
        private DatabaseHandler databaseHandler;
        private Difficulty difficulty;
        private int hints;
        public ConsoleUI() 
        {
        }

        public void Run() 
        {
            ProcessInputDifficulty();
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

        private void ProcessInputDifficulty()
        {
            try
            {
                Console.WriteLine("Choose difficulty from following: Easy, Medium, Hard");
                string input = Console.ReadLine().ToLower();
                if(input == "easy") 
                {
                    difficulty = Difficulty.Easy;
                    hints = 5;
                }
                else if(input == "medium") 
                {
                    difficulty = Difficulty.Medium;
                    hints = 10;
                }
                else if(input == "hard") 
                {
                    difficulty = Difficulty.Hard;
                    hints = 15;
                }
                else
                {
                PrintError("You have chosen invalid difficulty. Try again.");
                ProcessInputDifficulty();
                }
            }
            catch(Exception)
            {
                PrintError("You have chosen invalid difficulty. Try again.");
                ProcessInputDifficulty();
            }
        }

        private void Initialize()
        {
            databaseHandler = new DatabaseHandler(difficulty);
            var chosenImage = databaseHandler.ChooseRandomImage();
            grid = new Grid(chosenImage);
            legend = new Legend(chosenImage);
        }

        private void ProcessInput()
        {
            Console.WriteLine($"(S)olve (C)lear (H)int: {hints}");
            Console.Write("(#)Mark or (.)Blank followed by x,y coordinates: ");
            try
            {
                string[] input = Console.ReadLine().ToLower().Split();
                var parsedInput = char.Parse(input[0]);

                if (parsedInput == 's')
                {
                    grid.Solve();
                    return;
                }
                else if (parsedInput == 'c')
                {
                    grid.Clear();
                }
                else if (parsedInput == 'h')
                {
                    if(hints == 0) 
                    {
                        PrintError("No more hints available");
                        return;
                    }
                    grid.RevealHint();
                    hints--;
                }
                else if(parsedInput == '#' || parsedInput == '.') 
                {
                    var x = int.Parse(input[1]);
                    var y = int.Parse(input[2]);

                    if(parsedInput == '#' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.MarkTile(x,y);
                    if(parsedInput == '.' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.BlankTile(x,y);
                } 
            }
            catch (Exception) 
            {
                PrintError("Wrong input!");
            }
        }

        public void Print()
        {
            Console.Write("  ");
            for (int i = 0; i < grid.xSize; i++) Console.Write("{0, 3}", i);
            Console.WriteLine("  ");
            for (int i = 0; i < grid.ySize; i++)
            {
                Console.Write("{0, 2}", i);
                for (int j = 0; j < grid.xSize; j++)
                {
                    Console.Write("  ");
                    if(grid.Tiles[i,j].Input == TileState.Colored) Console.Write("#");
                    else if (grid.Tiles[i,j].Input == TileState.Blank) Console.Write(".");
                    else if (grid.Tiles[i,j].Input == TileState.Hidden) Console.Write(" ");
                }
                Console.Write("| ");
                for (int j = 0; j < grid.xSize / 2 + 1; j++)
                {
                    if(legend.vertical[i,j] == 0) continue;
                    else Console.Write(String.Format("{0,-3}", legend.vertical[i,j]));
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            for (int i = 0; i < grid.xSize; i++) Console.Write("---");
            Console.WriteLine();
            for (int i = 0; i < grid.ySize / 2 + 1; i++)
            {
                Console.Write("  ");
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

