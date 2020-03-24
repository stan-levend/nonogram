using System;
using System.Collections.Generic;
using nonogram.States;

namespace nonogram.Database
{
    public class DatabaseHandler
    {
        private List<char[,]> list = new List<char[,]>();
        public DatabaseHandler(Difficulty difficulty)
        {
            if(difficulty == (Difficulty.Easy)) list = new DatabaseEasy().List;
            else if(difficulty == (Difficulty.Medium)) list = new DatabaseMedium().List;
            else if(difficulty == (Difficulty.Hard)) list = new DatabaseHard().List;
        }        
        public char[,] ChooseRandomImage()
        {            
            var random = new Random();
            int index = random.Next(list.Count);
            return list[index];
        }
    }
}