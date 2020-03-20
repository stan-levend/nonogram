using System;
using nonogram.Core;
using nonogram.States;

namespace nonogram.ConsoleUI
{
    public class ConsoleUI {
        private Grid grid;
        private Database database;
        private static int hints = 10;
        public ConsoleUI() 
        {
            database = new Database();
        }
        public void Run() 
        {
            var chosenImage = database.ChooseRandomImage();
            grid = new Grid(chosenImage);
            grid.CurrentState = GameState.Playing;

            do 
            {
                grid.Print();
                ProcessInput();
            } 
            while (grid.CurrentState == GameState.Playing);

            grid.Print();
            if(grid.CurrentState == GameState.Solved) Console.WriteLine("Congratulations, you have solved the puzzle");
            else if(grid.CurrentState == GameState.Lost) Console.WriteLine("Better luck next time.");
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
                else if (parsedInput == 'h' && hints > 0)
                {
                    hints--;
                    grid.RevealHint();
                }
                else if(parsedInput == '#' || parsedInput == '.') 
                {
                    var x = int.Parse(input[1]);
                    var y = int.Parse(input[2]);

                    if(parsedInput == '#' && grid.xSizeGrid > x && grid.ySizeGrid > y && x >= 0 && y >= 0) grid.MarkTile(x,y);
                    if(parsedInput == '.' && grid.xSizeGrid > x && grid.ySizeGrid > y && x >= 0 && y >= 0) grid.BlankTile(x,y);
                } 
                grid.IsSolved();
            }
            catch (Exception) 
            {
                PrintError("Wrong input!");
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

