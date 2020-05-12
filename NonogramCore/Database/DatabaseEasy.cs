using System;
using System.Collections.Generic;

namespace nonogram.Database
{
    public class DatabaseEasy
    {
        public List<Image> List { get; private set; } = new List<Image>();
        public List<string> NameList { get; private set; } = new List<string>();


        public DatabaseEasy()
        {
            List.Add(new Dog());
            List.Add(new Duckling());
            List.Add(new Swan());
            NameList.Add(new Dog().Name);
            NameList.Add(new Duckling().Name);
            NameList.Add(new Swan().Name);
        }
    }
    [Serializable]
    public class Dog : Image
    {
        public Dog() {
            Name = "Dog";
            Data = new char[8, 8] 
            { 
                {'.', '.', '.', '.', '.', '#', '.', '.'},
                {'.', '.', '.', '.', '.', '#', '#', '.'},
                {'#', '.', '.', '.', '.', '#', '#', '#'},
                {'.', '#', '#', '#', '#', '#', '.', '.'},
                {'.', '#', '#', '#', '#', '#', '.', '.'},
                {'.', '#', '#', '.', '.', '#', '#', '.'},
                {'#', '#', '.', '.', '.', '#', '.', '#'},
                {'.', '#', '.', '.', '.', '#', '.', '.'}
            };
        }
    }
    [Serializable]
    public class Duckling : Image
    {
        public Duckling()
        {
            Name = "Duckling";
            Data = new char[9, 8] 
            { 
                {'.', '#', '#', '#', '.', '.', '.', '.'},
                {'#', '#', '.', '#', '.', '.', '.', '.'},
                {'.', '#', '#', '#', '.', '.', '#', '#'},
                {'.', '.', '#', '#', '.', '.', '#', '#'},
                {'.', '.', '#', '#', '#', '#', '#', '#'},
                {'#', '.', '#', '#', '#', '#', '#', '.'},
                {'#', '#', '#', '#', '#', '#', '.', '.'},
                {'.', '.', '.', '.', '#', '.', '.', '.'},
                {'.', '.', '.', '#', '#', '.', '.', '.'}
            };
        }

    }
    [Serializable]
    public class Swan : Image
    {
        public Swan() {
            Name = "Swan";
            Data = new char[6, 6] 
            { 
                {'#', '#', '.', '.', '.', '#'},
                {'.', '#', '.', '#', '#', '#'},
                {'.', '#', '.', '#', '#', '.'},
                {'.', '#', '#', '#', '.', '.'},
                {'.', '#', '#', '#', '#', '.'},
                {'.', '.', '.', '#', '.', '.'}
            };
        }
    }
}