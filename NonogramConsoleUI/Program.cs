using System;

namespace nonogram.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleUI = new ConsoleUI();
            consoleUI.Run();
            
            Console.ReadKey();
        }
    }
}
