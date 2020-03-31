using System;
using nonogram.Core;
using nonogram.Database;
using nonogram.Service;
using nonogram.Entity;

namespace nonogram.ConsoleUI
{
    public class ConsoleUI {
        private Grid grid;
        private Legend legend;
        private DatabaseHandler databaseHandler;
        private Difficulty difficulty;
        private Image chosenImage;
        private readonly IScoreService scoreService = new ScoreServiceFile();
        private static int hints = 10;

        public ConsoleUI() 
        {
        }

        public void Run() 
        {
            ProcessInputDifficulty();
            ProcessInputImage();
            Initialize();

            PrintScores();

            do 
            {
                PrintGrid();
                ProcessInput();
            } 
            while (grid.CurrentState == GameState.Playing);

            PrintGrid();
            if(grid.CurrentState == GameState.Won) 
            {
                Console.WriteLine("Congratulations, you have solved the puzzle");
                scoreService.AddScore(new Score{ImageName = chosenImage.Name, 
                                                Player = Environment.UserName, 
                                                Points = grid.GetScore()});
            }
            else if(grid.CurrentState == GameState.Lost) Console.WriteLine("Better luck next time.");
            PrintScores();
        }

        private void ProcessInputDifficulty()
        {
            try
            {
                Console.WriteLine("Choose difficulty from following: Easy, Medium, Hard");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "easy":
                        difficulty = Difficulty.Easy;
                        break;
                    case "medium":
                        difficulty = Difficulty.Medium;
                        break;
                    case "hard":
                        difficulty = Difficulty.Hard;
                        break;
                    default:
                        PrintError("You have chosen invalid difficulty. Try again.");
                        ProcessInputDifficulty();
                        break;
                }
            }
            catch(Exception)
            {
                PrintError("You have chosen invalid difficulty. Try again.");
                ProcessInputDifficulty();
            }
            databaseHandler = new DatabaseHandler(difficulty);
        }
        private void ProcessInputImage()
        {
            Console.Write("Choose from following images: ");
            foreach (var image in databaseHandler.List)
            {
                Console.Write("{0} ", image.Name);
            }
            Console.WriteLine();
            try
            {
                string input = Console.ReadLine().ToLower();
                chosenImage = null;
                foreach (var image in databaseHandler.List)
                {
                    if(input == image.Name.ToLower()) chosenImage = image;
                }
                if(chosenImage == null) 
                {
                    PrintError("You have chosen invalid image. Try again.");
                    ProcessInputImage();
                }
            }
            catch(Exception)
            {
                PrintError("You have chosen invalid image. Try again.");
                ProcessInputImage();
            }
        }

        private void Initialize()
        {
            grid = new Grid(chosenImage.Data);
            legend = new Legend(chosenImage.Data);
            
            grid.CurrentState = GameState.Playing;
        }

        private void ProcessInput()
        {
            Console.WriteLine($"(S)olve (C)lear (H)int: {hints}");
            Console.Write("(#)Mark or (.)Blank followed by (x y) coordinates: ");
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
                    else if(parsedInput == '.' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.BlankTile(x,y);
                    else PrintError("Wrong input!");
                } 
            }
            catch (Exception) 
            {
                PrintError("Wrong input!");
            }
        }

        public void PrintGrid()
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
                    if(legend.Vertical[i,j] == 0) continue;
                    else Console.Write(String.Format("{0,-3}", legend.Vertical[i,j]));
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
                    if(legend.Horizontal[i,j] == 0) 
                    {
                        Console.Write("   ");
                        zeros++;
                    }
                    else Console.Write(String.Format("{0,3}", legend.Horizontal[i,j]));
                }
                Console.WriteLine();
                if(zeros == grid.xSize) break;
            }
        }
        
        private void PrintScores()
        {
            Console.WriteLine("Top scores:");
            int index = 1;
            foreach (var score in scoreService.GetTopScores(chosenImage.Name))
            {
                Console.WriteLine("{0} {1} {2} {3}", index, score.ImageName, score.Player, score.Points);
                index++;
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

