using System.Collections.Generic;
using nonogram.Core;

namespace nonogram.Database
{
    public class DatabaseHandler
    {
        public List<Image> List { get; private set; } = new List<Image>();
        public List<string> NameList { get; private set; } = new List<string>(); //for front-end purposes - to pass only image names into a view, not all data
        
        public DatabaseHandler(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
            {
                List = new DatabaseEasy().List;
                NameList = new DatabaseEasy().NameList;
            }
            else if (difficulty == Difficulty.Medium)
            {
                List = new DatabaseMedium().List;
                NameList = new DatabaseMedium().NameList;
            }
            else if (difficulty == Difficulty.Hard)
            {
                List = new DatabaseHard().List;
                NameList = new DatabaseHard().NameList;
            }
        }
    }
}