using System;
using nonogram.Core;
using nonogram.Database;
using nonogram.Service;
using nonogram.Entity;

namespace nonogram.ConsoleUI
{
    public class ConsoleUI {
        private static ConsoleColor backgroundDefault = Console.BackgroundColor;
        private Grid grid;
        private Legend legend;
        private DatabaseHandler databaseHandler;
        private Difficulty difficulty;
        private Image chosenImage;
        //private readonly IScoreService scoreService = new ScoreServiceFile();
        private readonly IScoreService scoreService = new ScoreServiceEF();
        //private readonly ICommentService commentService = new CommentServiceFile();
        private readonly ICommentService commentService = new CommentServiceEF();
        //private readonly IRatingService ratingService = new RatingServiceFile();
        private readonly IRatingService ratingService = new RatingServiceEF();

        private static int hints = 10;

        public ConsoleUI() 
        {
        }

        public void Run() 
        {
            ChooseDifficulty();
            ChooseImage();
            Initialize();
            //scoreService.ClearScores();
            //commentService.ClearComments();
            //ratingService.ClearRating();
            PrintTopScores();

            do 
            {
                PrintGrid();
                ProcessInput();
            } 
            while (grid.CurrentState == GameState.Playing);

            PrintGrid();
            if(grid.CurrentState == GameState.Won) 
            {
                AddScore();
                Console.WriteLine("Congratulations, you have solved the puzzle");
            }
            else if(grid.CurrentState == GameState.Lost) Console.WriteLine("Better luck next time.");

            //AddScore();
            AddComment();
            AddRating();
            
            PrintTopScores();
            PrintLatestComments();
            PrintAllTimeRating();
        }

        private void ChooseDifficulty()
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
                        ChooseDifficulty();
                        break;
                }
            }
            catch(Exception)
            {
                PrintError("You have chosen invalid difficulty. Try again.");
                ChooseDifficulty();
            }
            databaseHandler = new DatabaseHandler(difficulty);
        }
        private void ChooseImage()
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
                    ChooseImage();
                }
            }
            catch(Exception)
            {
                PrintError("You have chosen invalid image. Try again.");
                ChooseImage();
            }
        }

        private void Initialize()
        {
            grid = new Grid(chosenImage.Data);
            legend = new Legend(chosenImage.Data);
        }

        private void ProcessInput()
        {
            Console.WriteLine($"'S' - Solve grid, 'C' - Clear grid, 'H' - Random hint, {hints} available.");
            Console.WriteLine("'M' - Mark / 'X' - Blank tile followed by 'x y' coordinates of the tile. Example: M 0 2");
            try
            {
                string[] input = Console.ReadLine().ToLower().Split();
                char parsedInput = char.Parse(input[0]);

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
                else if(parsedInput == 'm' || parsedInput == 'x') 
                {
                    var x = int.Parse(input[1]);
                    var y = int.Parse(input[2]);

                    if(parsedInput == 'm' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.MarkTile(x,y);
                    else if(parsedInput == 'x' && grid.xSize > x && grid.ySize > y && x >= 0 && y >= 0) grid.BlankTile(x,y);
                }
                else 
                {
                    PrintError("Wrong input!");
                    ProcessInput();
                }
            }
            catch (Exception) 
            {
                PrintError("Wrong input!");
                ProcessInput();
            }
        }

        public void PrintGrid()
        {
            //print X axis
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < grid.xSize; i++) Console.Write("{0, 3}", i);
            Console.WriteLine("  ");
            Console.WriteLine();
            //print Y axis, Grid, VerticalLegend
            for (int i = 0; i < grid.ySize; i++)
            {
                Console.Write("{0, 2}", i);
                for (int j = 0; j < grid.xSize; j++)
                {
                    Console.Write("  ");
                    if(grid.Tiles[i,j].Input == TileState.Colored) 
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        Console.BackgroundColor = backgroundDefault;
                    }
                    else if (grid.Tiles[i,j].Input == TileState.Blank) Console.Write("X");
                    else if (grid.Tiles[i,j].Input == TileState.Hidden) Console.Write(".");
                }
                Console.Write("  ");
                for (int j = 0; j < grid.xSize / 2 + 1; j++)
                {
                    if(legend.Vertical[i,j] == 0) continue;
                    else Console.Write(String.Format("{0,-3}", legend.Vertical[i,j]));
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            //print HorizontalLegend
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

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }




        private void AddScore() 
        {
            scoreService.AddScore(new Score{ImageName = chosenImage.Name, 
                                            Player = Environment.UserName, 
                                            Points = grid.GetScore()});
        }
        private void AddComment()
        {
            Console.Write("Leave a comment about the Nonogram game: ");
            commentService.AddComment(new Comment{ImageName = chosenImage.Name, 
                                                  Player = Environment.UserName, 
                                                  Text = Console.ReadLine()});
        }
        private void AddRating()
        {
            Console.Write("How many Stars would you give this game? (1-5) ");
            ratingService.AddRating(new Rating{Player = Environment.UserName, 
                                               Value = int.Parse(Console.ReadLine())});
        }
        private void PrintTopScores()
        {
            Console.WriteLine("Top scores for this image:");
            int index = 1;
            foreach (var score in scoreService.GetTopImageScores(chosenImage.Name))
            {
                Console.WriteLine("{0}. {1}, {2}: {3} points", index, score.ImageName, score.Player, score.Points);
                index++;
            }
            Console.WriteLine();
        }
        private void PrintLatestComments()
        {
            Console.WriteLine("Latest comments about the game:");
            int index = 1;
            foreach (var comment in commentService.GetLatestComments())
            {
                Console.WriteLine("{0}. {1}, {2}: {3}", index, comment.ImageName, comment.Player, comment.Text);
                index++;
            }
            Console.WriteLine();
        }
        private void PrintAllTimeRating()
        {
            for (int i = 5; i > 0; i--)
            {
                Console.Write("{0} ", ratingService.GetAllTimeRating(i));
                for (int j = i; j > 0 ; j--) Console.Write("*");
                Console.WriteLine();
            }
        }
    }
}

