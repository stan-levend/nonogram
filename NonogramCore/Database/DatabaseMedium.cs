using System.Collections.Generic;

namespace nonogram.Database
{
    public class DatabaseMedium
    {
        public List<Image> List { get; private set; } = new List<Image>();

        public DatabaseMedium()
        {
            List.Add(new Teacan());
            List.Add(new Human());
            List.Add(new Turtle());
            List.Add(new Bird());
        }
    }

    public class Teacan : Image
    {
        public Teacan() {
            Name = "Teacan";
            Data = new char[10, 10] 
            { 
                {'.', '.', '.', '.', '.', '#', '.', '.', '.', '.',},
                {'.', '.', '.', '#', '#', '#', '#', '#', '.', '.',},
                {'.', '.', '.', '#', '#', '#', '#', '#', '.', '.',},
                {'#', '#', '.', '#', '.', '#', '#', '#', '#', '#',},
                {'.', '#', '.', '#', '#', '#', '#', '#', '.', '#',},
                {'.', '#', '.', '#', '.', '#', '#', '#', '.', '#',},
                {'.', '#', '#', '#', '.', '#', '#', '#', '.', '#',},
                {'.', '#', '#', '#', '#', '#', '#', '#', '#', '#',},
                {'.', '.', '.', '#', '#', '#', '#', '#', '.', '.',},
                {'.', '.', '.', '#', '#', '#', '#', '#', '.', '.',} 
            };
        }
    }
    public class Turtle : Image
    {
        public Turtle() {
            Name = "Turtle";
            Data = new char[10, 10] 
            { 
                {'.', '.', '.', '.', '.', '.', '#', '.', '#', '#',},
                {'.', '.', '.', '.', '.', '.', '.', '.', '#', '#',},
                {'.', '.', '.', '.', '.', '.', '.', '#', '.', '.',},
                {'.', '.', '.', '.', '.', '.', '.', '.', '.', '#',},
                {'.', '.', '.', '.', '.', '#', '#', '.', '.', '.',},
                {'#', '#', '.', '.', '#', '#', '#', '#', '.', '.',},
                {'#', '#', '.', '#', '#', '#', '#', '#', '#', '.',},
                {'.', '#', '#', '#', '#', '#', '#', '#', '#', '.',},
                {'.', '.', '.', '.', '#', '.', '.', '#', '.', '.',},
                {'.', '.', '.', '#', '#', '.', '#', '#', '.', '.',} 
            };
        }
    }

    public class Human : Image
    {
        public Human() {
            Name = "Human";
            Data = new char[10, 10] 
            { 
                {'.', '.', '.', '.', '.', '.', '#', '#', '.', '.',},
                {'.', '.', '.', '.', '.', '.', '#', '#', '.', '.',},
                {'.', '.', '#', '#', '#', '#', '#', '.', '.', '#',},
                {'.', '#', '#', '.', '#', '#', '#', '#', '#', '#',},
                {'.', '#', '.', '.', '#', '#', '.', '.', '.', '.',},
                {'.', '.', '.', '#', '#', '.', '.', '.', '.', '.',},
                {'.', '.', '.', '#', '#', '#', '#', '.', '.', '.',},
                {'.', '.', '#', '#', '.', '.', '#', '.', '.', '.',},
                {'.', '#', '#', '.', '.', '#', '#', '.', '.', '.',},
                {'#', '#', '.', '.', '#', '#', '.', '.', '.', '.',} 
            };
        }
    }

    public class Bird : Image
    {
        public Bird() {
            Name = "Bird";
            Data = new char[10, 10] 
            { 
                {'.', '.', '.', '.', '.', '.', '.', '.', '#', '#',},
                {'.', '.', '#', '#', '.', '.', '.', '#', '#', '#',},
                {'.', '#', '.', '#', '#', '.', '#', '#', '#', '#',},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '.',},
                {'.', '.', '#', '#', '#', '#', '#', '#', '.', '.',},
                {'.', '.', '.', '#', '#', '#', '#', '#', '#', '.',},
                {'.', '.', '.', '.', '#', '#', '#', '#', '#', '#',},
                {'.', '.', '.', '#', '.', '#', '.', '#', '#', '#',},
                {'.', '.', '.', '.', '.', '.', '.', '.', '#', '#',},
                {'.', '.', '.', '.', '.', '.', '.', '#', '#', '.',} 
            };
        }
    }
}