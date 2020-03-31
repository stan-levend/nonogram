using System.Collections.Generic;
using nonogram.Core;

namespace nonogram.Database
{
    public class DatabaseHandler
    {
        public List<Image> List { get; private set; }= new List<Image>();
        public DatabaseHandler(Difficulty difficulty)
        {
            if(difficulty == (Difficulty.Easy)) List = new DatabaseEasy().List;
            else if(difficulty == (Difficulty.Medium)) List = new DatabaseMedium().List;
            else if(difficulty == (Difficulty.Hard)) List = new DatabaseHard().List;
        }   
    }
}